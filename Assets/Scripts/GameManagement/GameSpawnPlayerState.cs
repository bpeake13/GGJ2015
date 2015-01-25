using UnityEngine;
using System.Collections;
using System.IO;

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

        BinaryWriter writer = GameController.Instance.RecordingWriter;

        writer.Write(GameplayStatistics.Instance.PlayerCount);
        foreach(PlayerController player in GameplayStatistics.Instance.IteratePlayers())
        {
            writer.Write(player.Index);
        }

        SwitchState(new PlayerActionStartState(p1));
    }

    public override void Update()
    {
    }

    public override void Exit()
    {
    }
}
