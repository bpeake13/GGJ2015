﻿using UnityEngine;
using System.Collections.Generic;

public class PieceStructure {

    //Hold a reference to the Game Controller so we can access its scripts.
    GameObject gameController;

    //Structure to hold the pieces.
    private IPiece[,] pieces;

    //Board size
    int boardWidth, boardHeight;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="boardWidth"></param>
    /// <param name="boardHeight"></param>
    public PieceStructure(GameObject gameController, int boardWidth, int boardHeight)
    {
        this.gameController = gameController;
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;

        //Initialize the array
        pieces = new IPiece[boardWidth, boardHeight];
    }

    /// <summary>
    /// Create a new piece on the board at a specific space.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public void CreatePiece(int x, int y, PieceType type)
    {
        //Create the visual for the piece
        GameObject pieceObject = GameObject.Instantiate(gameController.GetComponent<PieceObjectConverter>().GetObjectByType(type),
                                                        BoardGenerator.ConvertBoardSpaceToWorldSpace(x, y),
                                                        Quaternion.identity) as GameObject;

        //Create the piece to place on the tile
        IPiece newPiece = PieceObjectConverter.MakePiece(type, pieceObject);
        pieces[x, y] = newPiece;
    }

    /// <summary>
    /// Remove the piece at a specified position.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void RemovePiece(int x, int y)
    {
        pieces[x, y] = null;
    }

    /// <summary>
    /// Move the piece at a space to a new space.
    /// </summary>
    public void MovePiece(int x1, int y1, int x2, int y2)
    {
        //Handle picking up the items when the player moves through/on them.
        //Should sweep through positions between the old space and new spaces, but I'm lazy
        if(pieces[x2, y2] != null)
        {
            //If there is an item there
            if(pieces[x2, y2] is IItem)
            {
                //Pick it up.
                ((Player)pieces[x1, y1]).GetInventory().AddItem((IItem)pieces[x2, y2]);
            }
        }

        //Move the piece to the space.
        pieces[x2, y2] = pieces[x1, y1];
        pieces[x2, y2].GetVisual().transform.position = BoardGenerator.ConvertBoardSpaceToWorldSpace(x2, y2);

        
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
    /// Check if the space specified has any pieces on it.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool isSpaceEmpty(int x, int y)
    {
        return (pieces[x, y] == null) && (isSpaceOnBoard(x, y));
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
