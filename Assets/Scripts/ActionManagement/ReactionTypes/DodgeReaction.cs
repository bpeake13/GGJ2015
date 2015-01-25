using UnityEngine;
using System.Collections;
using System.IO;

public class DodgeReaction : AbsAction, IReaction
{

    public DodgeReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Spot;
    }

    public void PreAction(ReActionStatus reaction, ActionStatus status)
    {
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
