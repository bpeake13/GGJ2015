using System.IO;
using UnityEngine;


public enum EActionType
{
    None,
    Move,
    Strong,
    Wide,
    Lunge,
    Item
}

public enum EReActionType
{
    None,
    Move,
    Block,
    Bash,
    Spot,
    Item
}

public enum EActionDirection
{
    None,
    Up,
    Down,
    Left,
    Right
}

public interface IAction
{
    EActionType GetActionType();

    EActionDirection GetDirection();

    void ReSolve(ReActionStatus reaction, ActionStatus status);

    Quaternion GetSpriteOrientation(ActionStatus status);

    Vector2 GetSpritePosition(ActionStatus status);

    Vector2[] GetAffectedTiles(ActionStatus status);

    Color GetActionColor();
	
    void Serialize(BinaryWriter writer);

    void Deserialize(BinaryReader reader);
}

public class NoneAction : IAction
{

    public EActionType GetActionType()
    {
        return EActionType.None;
    }

    public EActionDirection GetDirection()
    {
        return EActionDirection.None;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        
    }

    public Quaternion GetSpriteOrientation(ActionStatus status) { return Quaternion.identity; }

    public Vector2 GetSpritePosition(ActionStatus status) { return Vector2.zero; }

    public Vector2[] GetAffectedTiles(ActionStatus status) { return new Vector2[] { }; }

    public Color GetActionColor() { return Color.white; }

    public void Serialize(BinaryWriter writer) { }

    public void Deserialize(BinaryReader reader) { }
}

public interface IReaction
{
    EReActionType GetReactionType();
    EActionDirection GetDirection();
    void Serialize(BinaryWriter writer);
    void Deserialize(BinaryReader reader);
    void PreAction(ReActionStatus reaction, ActionStatus status);
}

public class NoneReaction : IReaction
{
    public EReActionType GetReactionType()
    {
        return EReActionType.None;
    }

    public EActionDirection GetDirection()
    {
        return EActionDirection.None;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
        return;
    }

    public void Serialize(BinaryWriter writer)
    {

    }

    public void Deserialize(BinaryReader reader)
    {

    }
}