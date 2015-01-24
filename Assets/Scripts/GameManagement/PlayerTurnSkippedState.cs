using UnityEngine;
using System.Collections;

/// <summary>
/// State for handling when a players turn is skipped.
/// Plays the on screen message and goes to the next players action
/// </summary>
public class PlayerTurnSkippedState : GameState
{
    public PlayerTurnSkippedState(PlayerController player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        throw new System.InvalidProgramException();
    }

    public override void Exit()
    {
        throw new System.InvalidProgramException();
    }

    private PlayerController player;
}
