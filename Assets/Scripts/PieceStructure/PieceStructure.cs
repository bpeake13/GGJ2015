using UnityEngine;
using System.Collections.Generic;

public class PieceStructure {

    //Hold a reference to the Game Controller so we can access its scripts.
    GameObject gameController;

    //Structure to hold the pieces.
    private IPiece[,] pieces;

    //Board size
    int boardWidth, boardHeight;

    //Hold special references for the players so there are a set number of them;
    List<Player> players;

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

        //Initialize the player array;
        players = new List<Player>();
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
        IPiece newPiece = PieceFactory.MakePiece(type, pieceObject);
        pieces[x, y] = newPiece;

        


        //Keep track of all players that are created
        if(type == PieceType.player)
        {
            players.Add((Player)newPiece);
        }
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

    /// <summary>
    /// Start counting from 1 (player 1 and player 2, not player 0)
    /// </summary>
    /// <param name="playerNumber"></param>
    /// <returns></returns>
    public Player GetPlayer(int playerNumber)
    {
        //Just in case someone searches for "Player number 0", just assume they meant player 1.
        if(playerNumber == 0)
        {
            playerNumber++;
        }
        return players[playerNumber];
    }
}
