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
        GameplayStatistics gs = GameplayStatistics.Instance;

        Time.timeScale = 0.2f;//slow down time
        timePassed = 0f;

        foreach (PlayerController player in gs.IteratePlayers())
        {
            if (player == action.OwnerPlayer)
                continue;

            action.AddReaction(new ReActionStatus(player));
        }
    }

    public override void Update()
    {
        bool waiting = false;

        GameplayStatistics gs = GameplayStatistics.Instance;
        foreach(PlayerController player in gs.IteratePlayers())
        {
            if (player == action.OwnerPlayer)
                continue;

            if (!player.HasReAction())
                continue;

            ReActionStatus reaction = action.GetReaction(player);
            if (reaction.ReactionType.GetReactionType() != EReActionType.None)
                continue;

            bool reacted = player.TakeReAction(reaction);
            if (!reacted)
                waiting = true;
        }

        if(!waiting)
        {
            SwitchState(new ActionResolver(action));
            return;
        }

        timePassed += Time.unscaledDeltaTime;//run timer out of the time scale effect

        if(timePassed >= gs.MaxReactionTime)
        {
            SwitchState(new ActionResolver(action));
            return;
        }
    }

    public override void Exit()
    {
        Time.timeScale = 1f;
    }

    private ActionStatus action;

    private float timePassed;
}
