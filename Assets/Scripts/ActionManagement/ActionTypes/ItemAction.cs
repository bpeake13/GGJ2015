using UnityEngine;
using System.Collections;

public class ItemAction : AbsAction, IAction
{

    int inventorySlot;

    public ItemAction(EActionDirection direction, int inventorySlot)
        : base(direction)
    {
        this.inventorySlot = inventorySlot;
    }

    public EActionType GetActionType()
    {
        return EActionType.Item;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

        //Pass the responsibility of handling items off to the item class
        attacker.GetInventory().UseItemAtIndex(inventorySlot).Activate(reaction, status);
    }
}
