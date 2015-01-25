using UnityEngine;
using System.Collections.Generic;

public class PieceObjectConverter : MonoBehaviour {

    //Objects for each of the pieces.
    public GameObject player1Object;
    public GameObject player2Object;
    public GameObject wallObject;
    public GameObject healthPotionObject;

    public Sprite LungeSprite;
    public Sprite WideSprite;
    public Sprite MoveSprite;
    public Sprite StrongSprite;
    public Sprite ItemSprite;

    //Dictionary to convert from piece type to object
    private Dictionary<PieceType, GameObject> pieceObjectDictionary;
    private Dictionary<EActionType, Sprite> actionSpriteDictionary;
    private GameObject[] playerObjects = new GameObject[2];
    private int createdPlayers = 0;

    /// <summary>
    /// Create and set up the dictionary.
    /// </summary>
    void Start()
    {
        //Fill the dictionary
        pieceObjectDictionary = new Dictionary<PieceType, GameObject>()
        {
            { PieceType.player,             player1Object},
            { PieceType.wall,               wallObject},
            { PieceType.health_potion,      healthPotionObject},
        };

        actionSpriteDictionary = new Dictionary<EActionType, Sprite>()
        {
            { EActionType.Lunge,            LungeSprite},
            { EActionType.Move,             MoveSprite},
            { EActionType.Wide,             WideSprite},
            { EActionType.Strong,           StrongSprite},
            { EActionType.Item,             ItemSprite}
        };

        //Set the player objects
        playerObjects[0] = player1Object;
        playerObjects[1] = player2Object;
    }

    /// <summary>
    /// Getter for the game object of the given type.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetObjectByType(PieceType type)
    {
        if(type == PieceType.player)
        {
            return GetPlayerObject();
        }
        return pieceObjectDictionary[type];
    }

    /// <summary>
    /// Get the sprite to display for a certain action
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Sprite GetSpriteByAction(EActionType type)
    {
        return actionSpriteDictionary[type];
    }

    /// <summary>
    /// If we're creating a player, create the correct one.
    /// </summary>
    /// <returns></returns>
    public GameObject GetPlayerObject()
    {
        return playerObjects[createdPlayers++];
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
