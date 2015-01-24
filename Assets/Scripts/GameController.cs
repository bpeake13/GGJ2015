using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    //Public variables to change in the editor.
    public int BOARD_WIDTH;
    public int BOARD_HEIGHT;

    //Structure to hold the pieces that make up the board.
    PieceStructure pieces;

    //Create a structure to spawn items
    ItemSpawner itemSpawner;

	// Use this for initialization
	void Start () {
        //Set up the piece structure/board
        pieces = new PieceStructure(gameObject, BOARD_WIDTH, BOARD_HEIGHT);

        //Set up the Board Generator to build the playing field.
        BoardGenerator.Generate(pieces);

        //Initialize the item spawner
        itemSpawner = new ItemSpawner(pieces);
	}
	
	// Update is called once per frame
	void Update () {
	}

    /// <summary>
    /// Getter for the piece structure, holding the positions of all of the pieces on the board.
    /// </summary>
    /// <returns></returns>
    public PieceStructure GetPieceStructure()
    {
        return pieces;
    }
}
