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

        //Use my item
        IItem item = attacker.GetInventory().UseItemAtIndex(inventorySlot);
        if (item != null)
        {
            item.Activate(reaction, status);
        }
    }

    public Quaternion GetSpriteOrientation(ActionStatus status)
    {
        return Quaternion.identity;
    }

    public Vector2 GetSpritePosition(ActionStatus status)
    {
        return status.OwnerPlayer.GetPlayerPiece().GetPosition();
    }
}
