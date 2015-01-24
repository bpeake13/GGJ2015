using UnityEngine;
using System.Collections.Generic;

public class PieceObjectConverter : MonoBehaviour {

    //Objects for each of the pieces.
    public GameObject playerObject;
    public GameObject wallObject;
    public GameObject healthPotionObject;

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
            { PieceType.player,             playerObject},
            { PieceType.wall,               wallObject},
            { PieceType.health_potion,      healthPotionObject},
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

    /// <summary>
    /// Create a correct object based exlusively on the type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IPiece MakePiece(PieceType type, GameObject pieceObject)
    {
        if (type == PieceType.player)
        {
            return new Player(pieceObject);
        }
        else if (type == PieceType.wall)
        {
            return new Wall(pieceObject);
        }
        else if (type == PieceType.health_potion)
        {
            return new HealthPotion(pieceObject);
        }
        return null;
    }
}
