using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;



class PlaybackActionDecoderState : GameState
{
    public override void Enter()
    {
        BinaryReader reader = GameController.Instance.PlaybackReader;

        int checker = reader.PeekChar();

        if (checker == -1)
        {
            int deadCount = 0;
            int alivePlayer = -1;
            foreach (PlayerController player in GameplayStatistics.Instance.IteratePlayers())
            {
                if (player.GetPlayerPiece().isDead())
                    deadCount++;
                else
                    alivePlayer = player.Index;
            }

            SwitchState(new GameOverState(alivePlayer));
            return;
        }

        ActionStatus action = FormatterServices.GetUninitializedObject(typeof(ActionStatus)) as ActionStatus;
        action.Deserialize(reader);

        SwitchState(new PlaybackShowActionState(action));
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }

    public override void Exit()
    {
    }
}
