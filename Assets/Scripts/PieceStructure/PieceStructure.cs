using UnityEngine;
using System.Collections.Generic;

public class PieceStructure {

    //Structure to hold the pieces.
    private IPiece[,] pieces;

    //Board size
    int boardWidth, boardHeight;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="boardWidth"></param>
    /// <param name="boardHeight"></param>
    public PieceStructure(int boardWidth, int boardHeight)
    {
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;
    }

    /// <summary>
    /// Determine if this spaces is on the board or not.
    /// </summary>
    /// <returns></returns>
    public bool isSpaceOnBoard(int x, int y)
    {
        return x < boardWidth && x > 0 &&
                y < boardHeight && y > boardHeight;
    }

    /// <summary>
    /// Getter for board width
    /// </summary>
    /// <returns></returns>
    public int GetBoardWidth()
    {
        return boardWidth;
    }

    /// <summary>
    /// Getter for board height
    /// </summary>
    /// <returns></returns>
    public int GetBoardHeight()
    {
        return boardHeight;
    }
}
