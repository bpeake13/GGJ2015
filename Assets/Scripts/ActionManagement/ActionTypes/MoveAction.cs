using UnityEngine;
using System.Collections;

public class MoveAction : AbsAction, IAction
{


    public MoveAction(EActionDirection direction)
        : base(direction)
    {
    }

    public EActionType GetActionType()
    {
        return EActionType.Move;
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
        bool enemyInFront = (targetPosition == enemy.GetPosition());  //The enemy is in the way
        EReActionType enemyReaction = reaction.ReactionType.GetReactionType();

        //If you move into a spot dodge, swap the player positions
        if (enemyInFront && enemyReaction == EReActionType.Spot)
        {
            //Swap positions
            GameController.Instance.GetPieceStructure().SwapPiecePositions((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
        }
        //If nothing else is blocking the attacker, move him.
        else
        {
            if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)targetPosition.x, (int)targetPosition.y))
            {
                //Move the player
                GameController.Instance.GetPieceStructure().MovePiece((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                        (int)targetPosition.x, (int)targetPosition.y);
            }
        }
    }
}
