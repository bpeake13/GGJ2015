using UnityEngine;
using System.Collections;

public abstract class AbsPiece : IPiece {

    //What piece is this?
    protected PieceType type;
    
    //Hold reference to the game object
    GameObject visual;

    //Hold a reference to the position
    Vector2 position;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="visualObject"></param>
    public AbsPiece(GameObject visualObject)
    {
        this.visual = visualObject;
    }

    /// <summary>
    /// Get the piece type.
    /// </summary>
    /// <returns></returns>
    public PieceType GetPieceType()
    {
        return type;
    }

    /// <summary>
    /// Return the object representing this piece.
    /// </summary>
    /// <returns></returns>
    public GameObject GetVisual()
    {
        return visual;
    }

    /// <summary>
    /// Getter for position
    /// </summary>
    /// <returns></returns>
    public Vector2 GetPosition()
    {
        return position;
    }

    /// <summary>
    /// Setter for the position to keep the piece updated.
    /// </summary>
    /// <param name="newPosition"></param>
    public void SetPosition(Vector2 newPosition)
    {
        position = newPosition;
    }
}
