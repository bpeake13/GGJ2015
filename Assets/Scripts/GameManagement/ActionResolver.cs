using UnityEngine;
using System.Collections;

public class ActionResolver : GameState
{

    public ActionResolver(PlayerController player, ActionStatus action)
    {
        this.player = player;
        this.action = action;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        //Check the attempted moves in the ActionStatus to see the resulting movement.

        //Update the Piece Structure based on the results of the ActionStatus
        PieceStructure pieces = GameController.Instance.GetPieceStructure();

        //Go to the action state of the next player and let them have their turn.
        PlayerActionStartState next = new PlayerActionStartState(GameplayStatistics.Instance.GetNextPlayer(player));
        SwitchState(next);
    }

    public override void Exit()
    {
        //Since this is the last state, call the item spawner to signify that a turn has passed.
        GameController.Instance.GetItemSpawner().TurnPassed();
    }

    private PlayerController player;

    private ActionStatus action;
}
