using UnityEngine;
using System.Collections;

public class ItemReaction : AbsAction, IReaction {

    int inventoryIndex;

    public ItemReaction(EActionDirection direction, int inventoryIndex)
    : base(direction)
    {
        this.inventoryIndex = inventoryIndex;
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Item;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
        Player reactor = reaction.OwnerPlayer.GetPlayerPiece();

        //Use my item
        IItem item = reactor.GetInventory().UseItemAtIndex(inventoryIndex);
        if (item != null)
        {
            item.Activate(reaction, status);
        }
    }
}
