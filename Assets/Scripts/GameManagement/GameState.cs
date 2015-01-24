using UnityEngine;
using System.Collections;

/// <summary>
/// Derive from this class to make a state for the game
/// </summary>
public abstract class GameState
{
    /// <summary>
    /// Get the next state.
    /// </summary>
    /// <returns>Gets the next state to transition to, NULL if no transition has been set yet</returns>
    public GameState GetNext()
    {
        return next;
    }

    public abstract void Enter();

    public abstract void Update();

    public abstract void Exit();

    /// <summary>
    /// Used to signal the event of an event
    /// </summary>
    /// <param name="arg">An arg that can be used to pass in some information</param>
    public virtual void Signal(int arg)
    {

    }

    protected void SwitchState(GameState next)
    {
        this.next = next;
    }

    private GameState next;
}
