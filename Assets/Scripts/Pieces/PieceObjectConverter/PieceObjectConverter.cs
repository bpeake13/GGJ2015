using UnityEngine;
using System.Collections.Generic;

public class PieceObjectConverter : MonoBehaviour {

    //Objects for each of the pieces.
    public GameObject playerObject;
    public GameObject wallObject;

    //Dictionary to convert from piece type to object
    private Dictionary<PieceType, GameObject> pieceObjectDictionary;

    /// <summary>
    /// Create and set up the dictionary.
    /// </summary>
    void Start()
    {
        //Fill the dictionary
        pieceObjectDictionary = new Dictionary<PieceType, GameObject>()
        {
            { PieceType.player,  playerObject},
            { PieceType.wall,    wallObject},
        };
    }

    /// <summary>
    /// Getter for the game object of the given type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObjectByType(PieceType type)
    {
        return pieceObjectDictionary[type];
    }
}
