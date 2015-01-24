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

    /// <summary>
    /// Getter for position
    /// </summary>
    /// <returns></returns>
    Vector2 GetPosition();

    /// <summary>
    /// Setter for the position to keep the piece updated.
    /// </summary>
    /// <param name="newPosition"></param>
    void SetPosition(Vector2 newPosition);
}
