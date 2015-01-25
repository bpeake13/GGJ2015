using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

public class PlaybackResolverState : GameState
{
    public PlaybackResolverState(ActionStatus action)
    {
        this.action = action;
    }

    public override void Enter()
    {
        GuiStack.Instance.Clear();

        //Check the attempted moves in the ActionStatus to see the resulting movement.
        foreach (ReActionStatus reaction in action.GetAllReactions())
        {
            ResetPlayerState(reaction.OwnerPlayer);
            action.ActionType.ReSolve(reaction, action);
        }
    }

    public override void Update()
    {
        timer -= Time.unscaledDeltaTime;

        if(timer <= 0)
        {
            
            SwitchState(new PlaybackActionDecoderState());
        }
    }

    public override void Exit()
    {
        //Since this is the last state, call the item spawner to signify that a turn has passed.
        GameController.Instance.GetItemSpawner().TurnPassed();

        //Reset all the tiles to their original colors.
        GameController.Instance.GetPieceStructure().ResetTileColors();
    }

    /// <summary>
    /// Reset the state variables for the specified player
    /// </summary>
    /// <param name="player"></param>
    private void ResetPlayerState(PlayerController player)
    {
        player.SetHasReAction(true);
    }

    private ActionStatus action;

    private float timer = 1f;
}
