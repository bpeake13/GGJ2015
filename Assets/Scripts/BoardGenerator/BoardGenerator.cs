using UnityEngine;
using System.Collections;

public static class BoardGenerator {

    //Variables for spawning empty tiles.
    const int groundY = 0;                      //What is the z value of spawned tiles.
    const float tileSpacing = 0.5f;            //How far apart are the tiles.

    /// <summary>
    /// Procedurally assign pieces in the piece structure.
    /// </summary>
    public static void Generate(PieceStructure pieces)
    {

        for(int x = 0; x < pieces.GetBoardWidth(); x ++)
        {
            for (int y = 0; y < pieces.GetBoardHeight(); y++)
            {
                CreateTile(x, y);
            }
        }
    }

    /// <summary>
    /// Create an individual tile.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private static void CreateTile(float x, float y)
    {
        GameObject.Instantiate(Resources.Load("Tile", typeof(GameObject)), new Vector3(x + x * tileSpacing, groundY, y + y * tileSpacing), Quaternion.identity);
    }
}
