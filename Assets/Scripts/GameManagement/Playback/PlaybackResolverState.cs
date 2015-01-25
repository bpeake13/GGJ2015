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
    }

    private ActionStatus action;

    private float timer = 1f;
}
