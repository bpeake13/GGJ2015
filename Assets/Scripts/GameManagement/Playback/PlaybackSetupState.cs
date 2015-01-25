using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class PlaybackSetupState : GameState
{
    public override void Enter()
    {
        BinaryReader reader = GameController.Instance.PlaybackReader;

        int playerCount = reader.ReadInt32();
        for(int i = 0; i < playerCount; i++)
        {
            int p_index = reader.ReadInt32();
            PlayerController.CreatePlayer(p_index);
        }

        foreach(PlayerController player in GameplayStatistics.Instance.IteratePlayers())
        {
            player.Spawn();
        }

        SwitchState(new PlaybackActionDecoderState());
    }

    public override void Update()
    {
        throw new InvalidProgramException();
    }

    public override void Exit()
    {
        
    }
}
