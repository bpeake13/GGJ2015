using UnityEngine;
using System.Collections.Generic;

public class PieceStructure {

    //Hold a reference to the Game Controller so we can access its scripts.
    GameObject gameController;

    //Structure to hold the pieces.
    private IPiece[,] pieces;
    private GameObject[,] tiles;

    //Board size
    int boardWidth, boardHeight;

    //The seed used at the time of creation
    int seed;

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

        this.seed = Random.seed;
    }

    /// <summary>
    /// Create a new piece on the board at a specific space.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="type"></param>
    public IPiece CreatePiece(int x, int y, PieceType type)
    {
        //Remove any piece that might already be there
        if(pieces[x, y] != null)
        {
            RemovePiece(x, y);
        }

        //Create the visual for the piece
        GameObject pieceObject = GameObject.Instantiate(gameController.GetComponent<PieceObjectConverter>().GetObjectByType(type),
                                                        BoardGenerator.ConvertBoardSpaceToWorldSpace(x, y),
                                                        Quaternion.identity) as GameObject;

        //Create the piece to place on the tile
        IPiece newPiece = PieceObjectConverter.MakePiece(type, pieceObject);
        pieces[x, y] = newPiece;
        pieces[x, y].SetPosition(new Vector2(x, y));
        return newPiece;
    }

    /// <summary>
    /// Remove the piece at a specified position.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void RemovePiece(int x, int y)
    {
        GameObject.Destroy(pieces[x, y].GetVisual());
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
                RemovePiece(x2, y2);
            }
        }

        //Move the piece to the space.
        pieces[x2, y2] = pieces[x1, y1];
        pieces[x1, y1] = null;
        pieces[x2, y2].SetPosition(new Vector2(x2, y2));
        pieces[x2, y2].GetVisual().SendMessage("SlideToTargetPosition", BoardGenerator.ConvertBoardSpaceToWorldSpace(x2, y2) );

        
    }

    /// <summary>
    /// Switch the places of two pieces on the board.
    /// </summary>
    public void SwapPiecePositions(int x1, int y1, int x2, int y2)
    {
        IPiece temp = pieces[x2, y2];

        pieces[x2, y2] = pieces[x1, y1];
        pieces[x2, y2].SetPosition(new Vector2(x2, y2));
        pieces[x2, y2].GetVisual().transform.position = BoardGenerator.ConvertBoardSpaceToWorldSpace(x2, y2);

        pieces[x1, y1] = temp;
        pieces[x1, y1].SetPosition(new Vector2(x1, y1));
        pieces[x1, y1].GetVisual().transform.position = BoardGenerator.ConvertBoardSpaceToWorldSpace(x1, y1);
    }

    /// <summary>
    /// Determine if this spaces is on the board or not.
    /// </summary>
    /// <returns></returns>
    public bool isSpaceOnBoard(int x, int y)
    {
        return x < boardWidth && x >= 0 &&
                y < boardHeight && y >= 0;
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
    /// Check if the space can be moved onto by a player
    /// </summary>
    /// <returns></returns>
    public bool isSpaceMovable(int x, int y)
    {
        return isSpaceOnBoard(x, y) && 
                (pieces[x, y] == null || 
                    (pieces[x, y].GetPieceType() != PieceType.player && pieces[x, y].GetPieceType() != PieceType.wall));
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
    /// Generate all of the tiles and keep track of the game objects.
    /// </summary>
    public void GenerateTiles()
    {
        tiles = BoardGenerator.Generate(this);
    }

    /// <summary>
    /// Color all of the specified tiles a specified color.
    /// </summary>
    /// <param name="affected"></param>
    /// <param name="color"></param>
    public void ColorTiles(Vector2[] affected, Color color)
    {
        foreach (Vector2 tile in affected)
        {
            if (isSpaceOnBoard((int)tile.x, (int)tile.y))
            {
                tiles[(int)tile.x, (int)tile.y].renderer.material.color = color;
            }
        }
    }

    /// <summary>
    /// Reset the color of all tiles back to white.
    /// </summary>
    public void ResetTileColors()
    {
        foreach (GameObject tile in tiles)
        {
            tile.renderer.material.color = Color.white;
        }
	}
	
    public int GetSeed()
    {
        return seed;
    }
}
