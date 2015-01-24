using UnityEngine;
using System.Collections;
using System.Text;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Create a player with the specified index, will delete any player of the same index
    /// </summary>
    /// <param name="index">The index of the player to create</param>
    /// <returns>The new player</returns>
    public static PlayerController CreatePlayer(int index)
    {
        if (index < 0)
            return null;

        GameplayStatistics gs = GameplayStatistics.Instance;
        PlayerController player = gs.GetPlayer(index);
        if(player)
        {
            gs.UnregisterPlayer(index);
            Destroy(player.gameObject);
        }

        GameObject newPlayerObj = new GameObject("Player_" + index, typeof(PlayerController));
        PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
        newController.Index = index;

        return newController;
    }

    /// <summary>
    /// Gets or sets the index of the player
    /// </summary>
    /// <remarks>This will re-register the player</remarks>
    public int Index
    {
        get { return index; }
        set
        {
            GameplayStatistics statistics = GameplayStatistics.Instance;
            if (statistics == null)
                return;

            statistics.UnregisterPlayer(index);
            if(value >= 0)
                statistics.RegisterPlayer(value, this);
            index = value;
        }
    }

    /// <summary>
    /// Checks to see if this player has an action this turn.
    /// </summary>
    public bool HasAction
    {
        get { return !bSkipAction; }
        set { bSkipAction = !value; }
    }

    /// <summary>
    /// Checks to see if this player has a reaction this turn
    /// </summary>
    public bool HasReAction
    {
        get { return !bSkipReAction; }
        set { bSkipAction = !value; }
    }

    /// <summary>
    /// Spawns the player object for this controller
    /// </summary>
    public virtual void Spawn()
    {
        //Spawn the players into the piece structure.
        GameObject gameController = GameObject.Find("GameController");
        PieceStructure pieces = gameController.GetComponent<GameController>().GetPieceStructure();

        BoardGenerator.SpawnPlayer(pieces, GameplayStatistics.Instance.PlayerCount);
    }

    /// <summary>
    /// Called when the player is registered with the game
    /// </summary>
    public virtual void OnRegistered()
    {

    }

    /// <summary>
    /// Called when the player is unregistered with the game
    /// </summary>
    public virtual void OnUnregistered()
    {
        
    }

    /// <summary>
    /// Called every frame that the player can take an action
    /// </summary>
    /// <returns>True if the player has taken there action, false otherwise</returns>
    public virtual bool TakeAction(ActionStatus action)
    {
        action.ActionType = new NoneAction();
        return true;
    }

    /// <summary>
    /// Called every frame the player can take a reaction.
    /// </summary>
    /// <returns>True when the player has finished the reaction.</returns>
    public virtual bool TakeReAction(ReActionStatus action)
    {
        action.ReactionType = new NoneReaction();
        return true;
    }

    protected string GetButtonLongName(string buttonShortName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Joystick");
        sb.Append(index);
        sb.Append(buttonShortName);

        return sb.ToString();
    }

    private int index = -1;

    private bool bSkipAction;
    private bool bSkipReAction;
}
