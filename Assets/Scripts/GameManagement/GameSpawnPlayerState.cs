using UnityEngine;
using System.Collections;

/// <summary>
/// Spawns the players and starts the game.
/// </summary>
public class GameSpawnPlayerState : GameState
{
    public override void Enter()
    {
        PlayerController p1 = PlayerController.CreatePlayer(0);
        PlayerController p2 = PlayerController.CreatePlayer(1);
        p1.Spawn();
        p2.Spawn();

        SwitchState(new PlayerActionStartState(p1));
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
