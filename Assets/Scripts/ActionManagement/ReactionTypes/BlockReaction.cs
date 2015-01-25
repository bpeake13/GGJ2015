using UnityEngine;
using System.Collections;
using System.IO;

public class BlockReaction : AbsAction, IReaction
{

    public BlockReaction(EActionDirection direction)
    : base(direction)
    {
    }

    public EReActionType GetReactionType()
    {
        return EReActionType.Block;
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
