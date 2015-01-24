using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Holds information about the current actions happening
/// </summary>
[System.Serializable]
public class ActionStatus
{
    public int PlayerIndex
    {
        get { return ownerPlayerIndex; }
    }

    public PlayerController OwnerPlayer
    {
        get { return ownerPlayer; }
    }

    public IAction ActionType
    {
        get { return actionType; }
        set { actionType = value; }
    }

    public EActionDirection Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public ActionStatus(PlayerController ownerPlayer)
    {
        this.reactionTable = new Dictionary<PlayerController,ReActionStatus>();

        this.ownerPlayer = ownerPlayer;
        this.ownerPlayerIndex = ownerPlayer.Index;

        //Initialize lists
        allReactions = new List<ReActionStatus>();
        reactionTable = new Dictionary<PlayerController, ReActionStatus>();
    }

    public void AddReaction(ReActionStatus reaction)
    {
        this.allReactions.Add(reaction);
        this.reactionTable.Add(reaction.OwnerPlayer, reaction);
    }

    public ReActionStatus GetReaction(PlayerController player)
    {
        if(reactionTable == null)
        {
            reactionTable = new Dictionary<PlayerController, ReActionStatus>();
            GameplayStatistics gs = GameplayStatistics.Instance;

            foreach(PlayerController p in gs.IteratePlayers())
            {
                ReActionStatus foundReaction = allReactions.Find(x => x.OwnerPlayer == p);
                reactionTable.Add(p, foundReaction);
            }
        }

        ReActionStatus reaction = null;
        reactionTable.TryGetValue(player, out reaction);
        return reaction;
    }

    public ReActionStatus[] GetAllReactions()
    {
        return allReactions.ToArray();
    }

    public int GetItemSlot()
    {
        return itemSlot;
    }

    private PlayerController ownerPlayer;

    [SerializeField]
    private int ownerPlayerIndex;

    [SerializeField]
    private List<ReActionStatus> allReactions;

    private Dictionary<PlayerController, ReActionStatus> reactionTable;

    [SerializeField]
    private IAction actionType;

    [SerializeField]
    private EActionDirection direction;

    [SerializeField]
    private int itemSlot;
}

[System.Serializable]
public class ReActionStatus
{
    public int PlayerIndex
    {
        get { return ownerPlayerIndex; }
    }

    public PlayerController OwnerPlayer
    {
        get { return ownerPlayer; }
    }

    public float OffsetTime
    {
        get { return offsetTime; }
        set { offsetTime = value; }
    }

    public IReaction ReactionType
    {
        get { return reactionType; }
        set { reactionType = value; }
    }

    public EActionDirection Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public ReActionStatus(PlayerController ownerPlayer)
    {
        this.ownerPlayer = ownerPlayer;
        this.ownerPlayerIndex = ownerPlayer.Index;
    }

    private PlayerController ownerPlayer;

    [SerializeField]
    private int ownerPlayerIndex;

    [SerializeField]
    private float offsetTime;

    [SerializeField]
    private IReaction reactionType;

    [SerializeField]
    private EActionDirection direction;
}
