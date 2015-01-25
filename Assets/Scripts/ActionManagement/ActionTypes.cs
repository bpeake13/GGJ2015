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

    //Quaternion GetSpriteOrientation();

    //Vector2 GetSpritePosition();
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

    public Quaternion GetSpriteOrientation() { return Quaternion.identity; }

    public Vector2 GetSpritePosition() { return Vector2.zero; }
}

public interface IReaction
{
    EReActionType GetReactionType();
    EActionDirection GetDirection();
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
}