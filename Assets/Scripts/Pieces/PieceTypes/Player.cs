using UnityEngine;
using System.Collections;

public class Player : AbsPiece {

    //State variables
    const int MAX_HEALTH = 5;
    int currentHealth;
    Inventory inventory;
    bool hasAction = true;
    bool hasReaction = true;

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
    /// Deal some damage to the player.
    /// </summary>
    /// <param name="amount"></param>
    public void Damage(int amount)
    {
        currentHealth -= amount;
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
    /// Determine if this player is dead or not.
    /// </summary>
    /// <returns></returns>
    public bool isDead()
    {
        return currentHealth > 0;
    }

    /// <summary>
    /// Getter
    /// </summary>
    /// <returns></returns>
    public bool HasAction()
    {
        return hasAction;
    }

    /// <summary>
    /// Getter
    /// </summary>
    /// <returns></returns>
    public bool HasReaction()
    {
        return hasReaction;
    }

    /// <summary>
    /// Setter
    /// </summary>
    /// <param name="var"></param>
    public void SetHasAction(bool var)
    {
        hasAction = var;
    }

    /// <summary>
    /// Setter
    /// </summary>
    /// <param name="var"></param>
    public void SetHasReaction(bool var)
    {
        hasReaction = var;
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
