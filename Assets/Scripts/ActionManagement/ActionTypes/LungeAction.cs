using UnityEngine;
using System.Collections;

public class LungeAction : AbsAction, IAction {

    //Statistics
    const int DAMAGE = 1;

    //Keep track of the direction of this attack
    EActionDirection direction;

    public LungeAction(EActionDirection direction)
    {
        this.direction = direction;
    }

	public EActionType GetActionType()
    {
        return EActionType.Lunge;
    }

    public EActionDirection GetDirection()
    {
        return direction;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Start by allowing the enemy to move
        EActionDirection enemyDirection = reaction.Direction;
        EnemyMoveReaction(enemy.GetPosition(), GetTargetPosition(enemy.GetPosition(), enemyDirection));

        //Perform calculations to assess the action.
        Vector2 targetPosition = GetTargetPosition(attacker.GetPosition(), direction);
        bool enemyInFront = EnemyBlockingPlayer(targetPosition, enemy.GetPosition());  //The enemy is 1 space away, blocking the movement
        bool enemyAtAttackDistance = EnemyBlockingPlayer(GetTargetPosition(targetPosition, direction), enemy.GetPosition()); //The enemy is 2 spaces away and will get hit with the attack
        EReActionType enemyReaction = reaction.ReactionType.GetReactionType();

        //If enemy blocking path and dodging, swap their positions
        if (enemyInFront && enemyReaction == EReActionType.Spot)
        {
            //Swap positions
            GameController.Instance.GetPieceStructure().SwapPiecePositions((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
        }
        //If no one is in the way, move forward.
        else if(!enemyInFront)
        {
            GameController.Instance.GetPieceStructure().MovePiece((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
        }

        //Now handle the damaging
        if(enemyReaction != EReActionType.Bash && enemyReaction != EReActionType.Spot && enemyReaction != EReActionType.Block)
        {
            enemy.Damage(DAMAGE);
        }
        //Handle stunning - If the attack connects, the enemy loses their next action
        if (enemyAtAttackDistance && (enemyReaction != EReActionType.Spot))
        {
            enemy.SetHasAction(false);
        }
        //Otherwise, if the attacker misses in any way, the attacker loses a reaction.
        else if((enemyAtAttackDistance && enemyReaction == EReActionType.Spot) || 
                !enemyAtAttackDistance)
        {
            attacker.SetHasReaction(false);
        }
    }

    /// <summary>
    /// Determine if an enemy is adjacent to the player or not.
    /// </summary>
    /// <returns></returns>
    private bool EnemyBlockingPlayer(Vector2 targetPosition, Vector2 enemy)
    {
        //Return true if there is an enemy blocking the player.
        return targetPosition == enemy;
    }
}
