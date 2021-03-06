﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization;

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

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(ownerPlayerIndex);
        writer.Write((char)direction);
        writer.Write(itemSlot);

        writer.Write(actionType.GetType().FullName);
        actionType.Serialize(writer);

        int reactionCount = allReactions.Count;
        writer.Write(reactionCount);
        for(int i = 0; i < reactionCount; i++)
        {
            allReactions[i].Serialize(writer);
        }
    }

    public void Deserialize(BinaryReader reader)
    {
        allReactions = new List<ReActionStatus>();
        reactionTable = new Dictionary<PlayerController, ReActionStatus>();

        this.ownerPlayerIndex = reader.ReadInt32();
        ownerPlayer = GameplayStatistics.Instance.GetPlayer(ownerPlayerIndex);
        this.direction = (EActionDirection)reader.ReadChar();
        this.itemSlot = reader.ReadInt32();

        System.Type aType = System.Type.GetType(reader.ReadString());
        actionType = FormatterServices.GetUninitializedObject(aType) as IAction;

        actionType.Deserialize(reader);

        int reactionCount = reader.ReadInt32();
        for(int i = 0; i < reactionCount; i++)
        {
            ReActionStatus reaction = FormatterServices.GetUninitializedObject(typeof(ReActionStatus)) as ReActionStatus;
            reaction.Deserialize(reader);

            allReactions.Add(reaction);
            reactionTable.Add(reaction.OwnerPlayer, reaction);
        }
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
        this.ReactionType = new NoneReaction();
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write(ownerPlayerIndex);
        writer.Write(offsetTime);
        writer.Write((char)direction);

        writer.Write(reactionType.GetType().FullName);
        reactionType.Serialize(writer);
    }

    public void Deserialize(BinaryReader reader)
    {
        this.ownerPlayerIndex = reader.ReadInt32();
        ownerPlayer = GameplayStatistics.Instance.GetPlayer(ownerPlayerIndex);

        this.offsetTime = reader.ReadSingle();

        this.direction = (EActionDirection)reader.ReadChar();

        System.Type rType = System.Type.GetType(reader.ReadString());
        reactionType = FormatterServices.GetUninitializedObject(rType) as IReaction;

        reactionType.Deserialize(reader);
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
