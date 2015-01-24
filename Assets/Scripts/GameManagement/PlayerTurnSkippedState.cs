using UnityEngine;
using System.Collections;

/// <summary>
/// State for handling when a players turn is skipped.
/// Plays the on screen message and goes to the next players action
/// </summary>
public class PlayerTurnSkippedState : GameState
{
    public PlayerTurnSkippedState(PlayerController player, ActionStatus action)
    {
        this.player = player;
        action.ActionType = EActionType.None;
    }

    public override void Enter()
    {
        player.HasAction = true;
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Signal(int arg)
    {
        PlayerController next = GameplayStatistics.Instance.GetNextPlayer(player);
        SwitchState(new PlayerActionStartState(next));
    }

    private PlayerController player;
}
