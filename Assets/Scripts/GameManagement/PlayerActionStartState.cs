using UnityEngine;
using System.Collections;

public class PlayerActionStartState : GameState
{
    public PlayerActionStartState(PlayerController player) : base()
    {
        this.player = player;
    }

    public override void Enter()
    {
        if(!player.HasAction)
        {
            SwitchState(new PlayerTurnSkippedState(player));
            return;
        }
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        
    }

    private PlayerController player;
}
