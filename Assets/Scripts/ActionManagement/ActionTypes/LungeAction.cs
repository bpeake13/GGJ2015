﻿using UnityEngine;
using System.Collections;
using System.IO;

public class LungeAction : AbsAction, IAction {

    //Statistics
    const int DAMAGE = 2;

    public LungeAction(EActionDirection direction):base(direction)
    {
    }

	public EActionType GetActionType()
    {
        return EActionType.Lunge;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

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
            SoundManager.Instance.PlaySound(SoundEffectType.dodge);
        }
        //If the enemy is in the way of the attacker's movement and the attack hits, push them back a space.
        else if (enemyInFront && enemyReaction != EReActionType.Spot)
        {
            //Push the enemy back a space.
            Vector2 pushBackSpace = GetTargetPosition(enemy.GetPosition(), direction);
            if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)pushBackSpace.x, (int)pushBackSpace.y))//Only push them back if there is no wall there.
            {
                GameController.Instance.GetPieceStructure().MovePiece((int)enemy.GetPosition().x, (int)enemy.GetPosition().y,
                                                                    (int)pushBackSpace.x, (int)pushBackSpace.y);
            }
        }
        //If no one is in the way, move forward.
        else if(!enemyInFront)
        {
            if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)targetPosition.x, (int)targetPosition.y))
            {
                GameController.Instance.GetPieceStructure().MovePiece((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
            }
            //If there is a wall in front of the player, play the invalid_move sound.
            else
            {
                SoundManager.Instance.PlaySound(SoundEffectType.invalid_move);
            }
        }

        //Now handle the damaging
        if ((enemyAtAttackDistance || enemyInFront) && enemyReaction != EReActionType.Bash && enemyReaction != EReActionType.Spot && enemyReaction != EReActionType.Block)
        {
            enemy.Damage(DAMAGE);
            SoundManager.Instance.PlaySound(SoundEffectType.damage);
        }
        //Handle stunning - If the attack connects, the enemy loses their next action
        if ((enemyAtAttackDistance || enemyInFront) && (enemyReaction != EReActionType.Spot))
        {
            enemy.SetHasAction(false);
            SoundManager.Instance.PlaySound(SoundEffectType.block);

            //If the reaction was a shield bash, push the players away from each other
            if(enemyReaction == EReActionType.Bash)
            {
                BashMovement(reaction, status);
            }
        }
        //Otherwise, if the attacker misses in any way, the attacker loses a reaction.
        else if((enemyAtAttackDistance && enemyReaction == EReActionType.Spot) || 
                !enemyAtAttackDistance)
        {
            attacker.SetHasReaction(false);
            SoundManager.Instance.PlaySound(SoundEffectType.dodge);
        }
    }

    public Quaternion GetSpriteOrientation(ActionStatus status)
    {
        return GetRotationByDirection(direction);
    }

    public Vector2 GetSpritePosition(ActionStatus status)
    {
        return status.OwnerPlayer.GetPlayerPiece().GetPosition();
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

    /// <summary>
    /// Get the tiles that are affected by this action and can be visually changed.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public Vector2[] GetAffectedTiles(ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Vector2 firstStep = GetTargetPosition(attacker.GetPosition(), direction);
        Vector2 secondStep = GetTargetPosition(firstStep, direction);

        return new Vector2[] {
            firstStep,
            secondStep
        };
    }

    /// <summary>
    /// Get the color that represents this action.
    /// </summary>
    /// <returns></returns>
    public Color GetActionColor()
    {
        return Color.yellow;
	}
	
    public void Serialize(BinaryWriter writer)
    {
        writer.Write((char)direction);
    }

    public void Deserialize(BinaryReader reader)
    {
        this.direction = (EActionDirection)reader.ReadChar();
    }
}
