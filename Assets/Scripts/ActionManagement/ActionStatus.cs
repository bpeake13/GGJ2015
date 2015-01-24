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

    public EActionType ActionType
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
        this.ownerPlayer = ownerPlayer;
        this.ownerPlayerIndex = ownerPlayer.Index;
    }

    public void AddReaction(ReActionStatus reaction)
    {
        this.allReactions.Add(reaction);
    }

    public ReActionStatus[] GetAllReactions()
    {
        return allReactions.ToArray();
    }

    private PlayerController ownerPlayer;

    [SerializeField]
    private int ownerPlayerIndex;

    [SerializeField]
    private List<ReActionStatus> allReactions;

    [SerializeField]
    private EActionType actionType;

    [SerializeField]
    private EActionDirection direction;


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

    public EReActionType ReactionType
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
    private EReActionType reactionType;

    [SerializeField]
    private EActionDirection direction;
}
