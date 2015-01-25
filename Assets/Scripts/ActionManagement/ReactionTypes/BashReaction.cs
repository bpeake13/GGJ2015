using UnityEngine;
using System.Collections;
using System.IO;

public class BashReaction : AbsAction, IReaction
{

    public BashReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Bash;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
        return;
    }

    public void Serialize(BinaryWriter writer)
    {
        writer.Write((char)direction);
    }

    public void Deserialize(BinaryReader reader)
    {
        this.direction = (EActionDirection)reader.ReadChar();
    }
}
