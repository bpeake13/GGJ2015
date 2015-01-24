using UnityEngine;
using System.Collections;

public class PlayerActionWaitState : GameState
{
    public PlayerActionWaitState(PlayerController player, ActionStatus action)
    {
        this.player = player;
        this.action = action;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        bool wasAction = player.TakeAction(action);

        if(wasAction)
        {
            PlayerReactionStartState next = new PlayerReactionStartState(action);
            SwitchState(next);
        }
    }

    public override void Exit()
    {
    }

    private PlayerController player;

    private ActionStatus action;
}
