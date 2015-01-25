using UnityEngine;
using System.Collections;

public class HealthPotion : AbsPiece, IItem
{
    //Attributes of this item
    int healAmount = 1;

    public HealthPotion(GameObject visual) : base(visual) { 
        this.type = PieceType.health_potion;
    }

    /// <summary>
    /// Heal the player when used.
    /// </summary>
    public void Activate(ReActionStatus reaction, ActionStatus status)
    {
        Player owner = status.OwnerPlayer.GetPlayerPiece();
        owner.Heal(healAmount);
    }

    public Sprite GetDisplaySprite()
    {
        return null;
    }
}
