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
        p1.Spawn();

        PlayerController.CreatePlayer(1).Spawn();

        SwitchState(new PlayerActionStartState(p1));
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
