using UnityEngine;
using System.Collections;

public class MoveReaction : AbsAction, IReaction
{

    public MoveReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Move;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player reactor = reaction.OwnerPlayer.GetPlayerPiece();

        //Start by allowing the enemy to move before the action
        EActionDirection reactorDirection = reaction.Direction;
        EnemyMoveReaction(reactor.GetPosition(), GetTargetPosition(reactor.GetPosition(), reactorDirection));
    }
}
