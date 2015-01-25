using UnityEngine;
using System.Collections;

public class DodgeReaction : AbsAction, IReaction
{

    public DodgeReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Spot;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
    }
}
