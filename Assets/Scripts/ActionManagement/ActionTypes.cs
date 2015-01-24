public enum EActionType
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

    void ReSolve(IReaction reaction);
}

public class NoneAction : IAction
{

    public EActionType GetActionType()
    {
        return EActionType.None;
    }

    public void ReSolve(IReaction reaction)
    {
        
    }
}

public interface IReaction
{
    EReActionType GetReactionType();
}

public class NoneReaction : IReaction
{
    public EReActionType GetReactionType()
    {
        return EReActionType.None;
    }
}