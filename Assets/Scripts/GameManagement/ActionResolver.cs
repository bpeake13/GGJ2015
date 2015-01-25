using UnityEngine;
using System.Collections;

public class ActionResolver : GameState
{

    public ActionResolver(ActionStatus action)
    {
        this.action = action;
    }

    public override void Enter()
    {
        action.OwnerPlayer.HasReAction = true;
    }

    public override void Update()
    {
        //Check the attempted moves in the ActionStatus to see the resulting movement.
        foreach (ReActionStatus reaction in action.GetAllReactions())
        {
            action.ActionType.ReSolve(reaction, action);
        }

        //Go to the action state of the next player and let them have their turn.
        PlayerActionStartState next = new PlayerActionStartState(GameplayStatistics.Instance.GetNextPlayer(action.OwnerPlayer));
        SwitchState(next);
    }

    public override void Exit()
    {
        //Since this is the last state, call the item spawner to signify that a turn has passed.
        GameController.Instance.GetItemSpawner().TurnPassed();
    }

    private ActionStatus action;
}
