using UnityEngine;
using System.Collections;

public class PlayerReactionStartState : GameState
{
    public PlayerReactionStartState(ActionStatus action)
    {
        this.action = action;
    }

    public override void Enter()
    {
        Time.timeScale = 0.2f;//slow down time
    }

    public override void Update()
    {
        GameplayStatistics gs = GameplayStatistics.Instance;
        foreach(PlayerController player in gs.IteratePlayers())
        {
            //player.TakeReAction();
        }
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
    }

    private ActionStatus action;
}
