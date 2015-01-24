using UnityEngine;
using System.Collections;

public interface IPiece {

    /// <summary>
    /// Get the piece type.
    /// </summary>
    /// <returns></returns>
    PieceType GetPieceType();

    /// <summary>
    /// Return the object representing this piece.
    /// </summary>
    /// <returns></returns>
    GameObject GetVisual();
}
