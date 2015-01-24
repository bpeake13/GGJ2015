using UnityEngine;
using System.Collections;

public class Player : AbsPiece {

    //State variables
    const int MAX_HEALTH = 5;
    int currentHealth;
    Inventory inventory;

    public Player(GameObject visual): base(visual)
    {
        //Create an empty inventory for the player.
        inventory = new Inventory();
    }

    /// <summary>
    /// Heal the player
    /// </summary>
    /// <param name="amount"></param>
    public void Heal(int amount)
    {
        //Heal up to max health
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MAX_HEALTH);
    }

    /// <summary>
    /// Getter for health.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    /// <summary>
    /// Getter
    /// </summary>
    /// <returns></returns>
    public Inventory GetInventory()
    {
        return inventory;
    }
}
