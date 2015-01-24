using UnityEngine;
using System.Collections;

public abstract class AbsPiece : IPiece {

    //What piece is this?
    protected PieceType type;

    /// <summary>
    /// Get the piece type.
    /// </summary>
    /// <returns></returns>
    public PieceType GetPieceType()
    {
        return type;
    }
}
