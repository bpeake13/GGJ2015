using UnityEngine;
using System.Collections;
using System.IO;

public class ActionResolver : GameState
{

    public ActionResolver(ActionStatus action)
    {
        this.action = action;
    }

    public override void Enter()
    {
        ResetPlayerState(action.OwnerPlayer);

        BinaryWriter writer = GameController.Instance.RecordingWriter;
        action.Serialize(writer);
    }

    public override void Update()
    {
        GuiStack.Instance.Clear();

        //Check the attempted moves in the ActionStatus to see the resulting movement.
        foreach (ReActionStatus reaction in action.GetAllReactions())
        {
            ResetPlayerState(reaction.OwnerPlayer);
            action.ActionType.ReSolve(reaction, action);
        }

        int deadCount = 0;
        int alivePlayer = -1;
        foreach(PlayerController player in GameplayStatistics.Instance.IteratePlayers())
        {
            if (player.GetPlayerPiece().isDead())
                deadCount++;
            else
                alivePlayer = player.Index;
        }

        //Go to the action state of the next player and let them have their turn.
        GameState next;
        if (deadCount != GameplayStatistics.Instance.PlayerCount - 1)
            next = new PlayerActionStartState(GameplayStatistics.Instance.GetNextPlayer(action.OwnerPlayer));
        else
            next = new GameOverState(alivePlayer);

        SwitchState(next);
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
}
