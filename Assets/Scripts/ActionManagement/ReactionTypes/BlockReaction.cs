using UnityEngine;
using System.Collections;

public class BlockReaction : AbsAction, IReaction
{

    public BlockReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Block;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
    }
}
