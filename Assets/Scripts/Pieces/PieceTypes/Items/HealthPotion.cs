using UnityEngine;
using System.Collections;

public class HealthPotion : AbsPiece, IItem
{
    //Attributes of this item
    int healAmount = 1;

    public HealthPotion(GameObject visual) : base(visual) { }

    /// <summary>
    /// Heal the player when used.
    /// </summary>
    public void Activate(Player owner)
    {
        owner.Heal(healAmount);
    }
}
