using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public int BOARD_WIDTH;
    public int BOARD_HEIGHT;

    //Structure to hold the pieces that make up the board.
    PieceStructure pieces;

	// Use this for initialization
	void Start () {
        //Set up the piece structure/board
        pieces = new PieceStructure(BOARD_WIDTH, BOARD_HEIGHT);

        //Set up the Board Generator to set up the playing field.
        BoardGenerator.Generate(pieces);
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
