using UnityEngine;
using System.Collections;

public class HealthPotion : AbsPiece, IItem
{
    //Attributes of this item
    int healAmount = 1;
    Player owner;

    public HealthPotion(GameObject visual) : base(visual) { }

    /// <summary>
    /// Heal the player when used.
    /// </summary>
    public void Activate()
    {
        owner.Heal(healAmount);
    }
}
