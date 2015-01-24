using UnityEngine;
using System.Collections;

public class PlayerActionStartState : GameState
{
    public PlayerActionStartState(PlayerController player) : base()
    {
        this.player = player;
        action = new ActionStatus(player);
    }

    public override void Enter()
    {
        if(!player.HasAction)
        {
            SwitchState(new PlayerTurnSkippedState(action));
            return;
        }
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Signal(int arg)
    {
        SwitchState(new PlayerActionWaitState(action));
    }

    private PlayerController player;
    private ActionStatus action;
}
