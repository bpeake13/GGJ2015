using UnityEngine;
using System.Collections.Generic;

public static class BoardGenerator {

    //Variables for spawning empty tiles.
    const int groundY = 0;                      //What is the z value of spawned tiles.
    const float tileSpacing = 0.15f;            //How far apart are the tiles.

    //Variables for procedurally placing walls
    const int chanceWallPerTile = 20; //1 in x chance

    //Spawn locations for the players
    //The first index is the number of players, the second index is the 
    static Dictionary<int, Vector2[]> playerSpawnLocations = new Dictionary<int, Vector2[]>
    {
        {2, new Vector2[]{new Vector2(4, 4), new Vector2(5, 5)}}
    };
    static int spawnedPlayerIndex = 0;

    /// <summary>
    /// Procedurally assign pieces in the piece structure.
    /// </summary>
    public static void Generate(PieceStructure pieces)
    {
        //Create all of the tiles that make up the board
        GameObject parentBoard = new GameObject();
        parentBoard.transform.name = "Board";
        for(int x = 0; x < pieces.GetBoardWidth(); x ++)
        {
            for (int y = 0; y < pieces.GetBoardHeight(); y++)
            {
                CreateTile(x, y, parentBoard.transform);
            }
        }

        //Use an algorithm to spawn pieces on the board.
        SpawnWallsOnBoard(pieces);
    }

    public static void Reset()
    {

    }

    /// <summary>
    /// Convert the board space into world space so we can instatiate objects at the same location.
    /// </summary>
    /// <param name="boardX"></param>
    /// <param name="boardY"></param>
    /// <returns></returns>
    public static Vector3 ConvertBoardSpaceToWorldSpace(int boardX, int boardY)
    {
        return new Vector3(boardX + boardX * tileSpacing, groundY, boardY + boardY * tileSpacing);
    }

    /// <summary>
    /// Create an individual tile.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private static void CreateTile(int x, int y, Transform parentBoard)
    {

        GameObject newTile = GameObject.Instantiate(Resources.Load("Tile", typeof(GameObject)), ConvertBoardSpaceToWorldSpace(x, y), Quaternion.identity) as GameObject;
        newTile.transform.parent = parentBoard;
    }

    /// <summary>
    /// Create the players we need to play the game.
    /// </summary>
    /// <param name="pieces"></param>
    public static Player SpawnPlayer(PieceStructure pieces, int numPlayers)
    {
        //Spawn the player based on its spawn position
        Vector2 position = playerSpawnLocations[numPlayers][spawnedPlayerIndex++];
        return (Player)pieces.CreatePiece((int)position.x, (int)position.y, PieceType.player);
    }

    /// <summary>
    /// Place walls on the board procedurally.
    /// </summary>
    /// <param name="pieces"></param>
    private static void SpawnWallsOnBoard(PieceStructure pieces)
    {
        //Randomly place walls
        for (int x = 0; x < pieces.GetBoardWidth(); x++)
        {
            for (int y = 0; y < pieces.GetBoardHeight(); y++)
            {
                //Random chance of placing a wall
                if(Random.Range(0, chanceWallPerTile) == 0)
                {
                    pieces.CreatePiece(x, y, PieceType.wall);
                }
            }
        }
    }
}
