using UnityEngine;
using System.Collections;

public abstract class AbsPiece : IPiece {

    //What piece is this?
    protected PieceType type;
    
    //Hold reference to the game object
    GameObject visual;

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
}
