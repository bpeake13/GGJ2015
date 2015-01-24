using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// General manager class for the game.
/// </summary>
public class GameplayStatistics: MonoBehaviour
{
    /// <summary>
    /// Gets the instance of GameplayStatistics
    /// </summary>
    public static GameplayStatistics Instance
    {
        get { return instance; }
    }

    public int PlayerCount
    {
        get { return playerTable.Count; }
    }

    public float MaxReactionTime
    {
        get { return maxReactionTime; }
        set { maxReactionTime = value; }
    }

    /// <summary>
    /// Registers a player with the game
    /// </summary>
    /// <param name="index">The index to register this player at.</param>
    /// <param name="controller">The controller to register.</param>
    /// <remarks>This method assumes that the player controller has an invalid index, if it has a valid index, then unexpected results could rise</remarks>
    public void RegisterPlayer(int index, PlayerController controller)
    {
        UnregisterPlayer(index);

        playerTable.Add(index, controller);

        controller.OnRegistered();
    }

    /// <summary>
    /// Unregisters a player with the game
    /// </summary>
    /// <param name="index">The index of the player to unregister</param>
    public void UnregisterPlayer(int index)
    {
        PlayerController controller = null;
        playerTable.TryGetValue(index, out controller);
        if (!controller)
            return;

        controller.OnUnregistered();
    }

    /// <summary>
    /// Get a registered player from the game.
    /// </summary>
    /// <param name="index">The index of the player to get</param>
    /// <returns>The player controller with that index, or null if none exists</returns>
    public PlayerController GetPlayer(int index)
    {
        PlayerController controller = null;
        playerTable.TryGetValue(index, out controller);
        if (!controller)
            return null;
        return controller;
    }

    /// <summary>
    /// Gets the next player in order from the specified starting player, this will loop around to the start.
    /// </summary>
    /// <param name="start">The starting player controller.</param>
    /// <returns>The next controller.</returns>
    public PlayerController GetNextPlayer(PlayerController start)
    {
        if (playerTable.Count == 0)
            return null;

        bool hasFound = false;
        foreach(KeyValuePair<int, PlayerController> playerEntry in playerTable)
        {
            if (!start)
                return playerEntry.Value;

            if (!hasFound)
            {
                if (playerEntry.Value == start)
                    hasFound = true;
            }
            else
                return playerEntry.Value;
        }

        return playerTable.Values.GetEnumerator().Current;
    }

    public IEnumerable<PlayerController> IteratePlayers()
    {
        foreach (KeyValuePair<int, PlayerController> playerEntry in playerTable)
        {
            yield return playerEntry.Value;
        }
    }

    /// <summary>
    /// Signals the current state that an event has happened.
    /// </summary>
    /// <param name="arg">An arg to pass to the state.</param>
    public void Signal(int arg)
    {
        state.Signal(arg);
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        state = new GameSpawnPlayerState();
        state.Enter();
    }

    void Update()
    {
        GameState next = state.GetNext();

        while (next != null)
        {
            state.Exit();
            next.Enter();
            next = state.GetNext();
            state = next;
        }

        state.Update();
    }

    private GameState state;

    private float maxReactionTime = 1f;

    private SortedDictionary<int, PlayerController> playerTable = new SortedDictionary<int, PlayerController>();

    private static GameplayStatistics instance;
}
