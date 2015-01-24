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
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player reactor = reaction.OwnerPlayer.GetPlayerPiece();

        //Use my item
        reactor.GetInventory().UseItemAtIndex(inventoryIndex).Activate(reaction, status);
    }
}
