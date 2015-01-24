﻿public enum EActionType
{
    None,
    Move,
    Strong,
    Wide,
    Lunge
}

public enum EReActionType
{
    None,
    Move,
    Block,
    Bash,
    Spot
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

    void ReSolve(IReaction reaction);
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

    public void ReSolve(IReaction reaction)
    {
        
    }
}

public interface IReaction
{
    EReActionType GetReactionType();
    EActionDirection GetDirection();
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
}