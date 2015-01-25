using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //Singlton setup
    public static GameController Instance
    {
        get { return instance; }
    }
    private static GameController instance;



    //Public variables to change in the editor.
    public int BOARD_WIDTH;
    public int BOARD_HEIGHT;

    //Structure to hold the pieces that make up the board.
    PieceStructure pieces;

    //Create a structure to spawn items
    ItemSpawner itemSpawner;

	// Use this for initialization
	void Start () {

        //Set the singleton
        instance = this;

        //Set up the piece structure/board
        pieces = new PieceStructure(gameObject, BOARD_WIDTH, BOARD_HEIGHT);

        //Set up the Board Generator to build the playing field.
        pieces.GenerateTiles();
        BoardGenerator.SpawnWallsOnBoard(pieces);

        //Initialize the item spawner
        itemSpawner = new ItemSpawner(pieces);
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(Input.GetKeyDown(KeyCode.Delete))
        {
            Application.LoadLevel(0);
        }
	}

    /// <summary>
    /// Getter for the piece structure, holding the positions of all of the pieces on the board.
    /// </summary>
    /// <returns></returns>
    public PieceStructure GetPieceStructure()
    {
        return pieces;
    }

    /// <summary>
    /// Getter for the item spawner
    /// </summary>
    /// <returns></returns>
    public ItemSpawner GetItemSpawner()
    {
        return itemSpawner;
    }
}
