using UnityEngine;
using System.Collections;

public class ItemAction : AbsAction, IAction
{


    public ItemAction(EActionDirection direction)
        : base(direction)
    {
    }

    public EActionType GetActionType()
    {
        return EActionType.Item;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

        //Pass the responsibility of handling items off to the item class
        attacker.GetInventory().UseItemAtIndex(0).Activate(reaction, status);
    }
}
