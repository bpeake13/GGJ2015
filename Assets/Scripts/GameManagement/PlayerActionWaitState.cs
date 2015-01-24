using UnityEngine;
using System.Collections;

public class PlayerActionWaitState : GameState
{
    public PlayerActionWaitState(PlayerController player)
    {
        this.player = player;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        bool wasAction = player.TakeAction();

        if(wasAction)
        {

        }
    }

    public override void Exit()
    {
    }

    private PlayerController player;
}
