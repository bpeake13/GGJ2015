using UnityEngine;
using System.Collections;

public class BashReaction : AbsAction, IReaction
{

    public BashReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Bash;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
        return;
    }
}
