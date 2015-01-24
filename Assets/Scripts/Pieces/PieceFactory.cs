using UnityEngine;
using System.Collections;

public static class PieceFactory {

    /// <summary>
    /// Create a correct object based exlusively on the type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
	public static IPiece MakePiece(PieceType type, GameObject pieceObject)
    {
        if(type == PieceType.player)
        {
            return new Player(pieceObject);
        }
        else if(type == PieceType.wall)
        {
            return new Wall(pieceObject);
        }
        return null;
    }
}
