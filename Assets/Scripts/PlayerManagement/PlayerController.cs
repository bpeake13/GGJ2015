﻿using UnityEngine;
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
        get { return playerPiece.HasAction(); }
        set { playerPiece.SetHasAction(!value); }
    }

    /// <summary>
    /// Checks to see if this player has a reaction this turn
    /// </summary>
    public bool HasReAction
    {
        get { return playerPiece.HasReaction(); }
        set { playerPiece.SetHasReaction(!value); }
    }

    public int Health
    {
        get { return health; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    /// <summary>
    /// Spawns the player object for this controller
    /// </summary>
    public virtual void Spawn()
    {
        //Spawn the players into the piece structure.
        GameObject gameController = GameObject.Find("GameController");
        PieceStructure pieces = gameController.GetComponent<GameController>().GetPieceStructure();

        playerPiece = BoardGenerator.SpawnPlayer(pieces, GameplayStatistics.Instance.PlayerCount);
    }

    /// <summary>
    /// Called when the player is registered with the game
    /// </summary>
    public virtual void OnRegistered()
    {
        button0Name = GetButtonLongName("Button0");
        button1Name = GetButtonLongName("Button1");
        button2Name = GetButtonLongName("Button2");
        button3Name = GetButtonLongName("Button3");
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
        if(Input.GetButtonDown(button0Name))
        {

        }
        else if(Input.GetButtonDown(button1Name))
        {

        }
        else if(Input.GetButtonDown(button2Name))
        {

        }
        else if(Input.GetButtonDown(button3Name))
        {

        }

        return true;
    }

    /// <summary>
    /// Called every frame the player can take a reaction.
    /// </summary>
    /// <returns>True when the player has finished the reaction.</returns>
    public virtual bool TakeReAction(ReActionStatus action)
    {
        if (Input.GetButtonDown(button0Name))
        {

        }
        else if (Input.GetButtonDown(button1Name))
        {

        }
        else if (Input.GetButtonDown(button2Name))
        {

        }
        else if (Input.GetButtonDown(button3Name))
        {

        }

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

    /// <summary>
    /// Getter for the player piece
    /// </summary>
    /// <returns></returns>
    public Player GetPlayerPiece()
    {
        return playerPiece;
    }

    private int index = -1;


    //Hold a reference to the player piece
    Player playerPiece;

    private bool bSkipAction;
    private bool bSkipReAction;

    private string button0Name;
    private string button1Name;
    private string button2Name;
    private string button3Name;

    private int health;
    private int maxHealth;
}
