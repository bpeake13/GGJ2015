﻿using UnityEngine;
using System.Collections;

public class StrongAction : AbsAction, IAction {

    //Statistics
    const int DAMAGE = 2;


    public StrongAction(EActionDirection direction): base(direction)
    {
    }

	public EActionType GetActionType()
    {
        return EActionType.Strong;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

        //Perform calculations to assess the action.
        Vector2 attackPosition = GetTargetPosition(attacker.GetPosition(), direction);
        bool enemyInFront = (attackPosition == enemy.GetPosition());  //The enemy will be punched
        EReActionType enemyReaction = reaction.ReactionType.GetReactionType();

        //If enemy is in the attack position, check if the attack does damage
        if (enemyInFront && enemyReaction != EReActionType.Spot && enemyReaction != EReActionType.Block && enemyReaction != EReActionType.Bash)
        {
            enemy.Damage(DAMAGE);
        }
        //If the reaction was a shiled bash, push the players away from each other
        else if (enemyInFront && enemyReaction == EReActionType.Bash)
        {
            BashMovement(reaction, status);
        }
        
    }

    public Quaternion GetSpriteOrientation(ActionStatus status)
    {
        return Quaternion.identity;
    }

    public Vector2 GetSpritePosition(ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Vector2 attackPosition = GetTargetPosition(attacker.GetPosition(), direction);
        return attackPosition;
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

        return new Vector2[] {
            firstStep
        };
    }

    /// <summary>
    /// Get the color that represents this action.
    /// </summary>
    /// <returns></returns>
    public Color GetActionColor()
    {
        return Color.black;
    }
}
