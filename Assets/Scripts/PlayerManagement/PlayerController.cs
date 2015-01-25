using UnityEngine;
using System.Collections;
using System.Text;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Create a player with the specified index, will delete any player of the same index
    /// </summary>
    /// <param name="index">The index of the player to create</param>
    /// <returns>The new player</returns>
    public static PlayerController CreatePlayer(int index)
    {
        if (index < 0)
            return null;

        GameplayStatistics gs = GameplayStatistics.Instance;
        PlayerController player = gs.GetPlayer(index);
        if(player)
        {
            gs.UnregisterPlayer(index);
            Destroy(player.gameObject);
        }

        GameObject newPlayerObj = new GameObject("Player_" + index, typeof(PlayerController));
        PlayerController newController = newPlayerObj.GetComponent<PlayerController>();
        newController.Index = index;

        return newController;
    }

    /// <summary>
    /// Gets or sets the index of the player
    /// </summary>
    /// <remarks>This will re-register the player</remarks>
    public int Index
    {
        get { return index; }
        set
        {
            GameplayStatistics statistics = GameplayStatistics.Instance;
            if (statistics == null)
                return;

            statistics.UnregisterPlayer(index);
            if(value >= 0)
                statistics.RegisterPlayer(value, this);
            index = value;
        }
    }

    /// <summary>
    /// Checks to see if this player has an action this turn.
    /// </summary>
    public bool HasAction
    {
        get { return playerPiece.HasAction(); }
        set { playerPiece.SetHasAction(!value); }
    }

    /// <summary>
    /// Checks to see if this player has a reaction this turn
    /// </summary>
    public bool HasReAction
    {
        get { return playerPiece.HasReaction(); }
        set { playerPiece.SetHasReaction(!value); }
    }

    public int Health
    {
        get { return health; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    /// <summary>
    /// Spawns the player object for this controller
    /// </summary>
    public virtual void Spawn()
    {
        //Spawn the players into the piece structure.
        GameObject gameController = GameObject.Find("GameController");
        PieceStructure pieces = gameController.GetComponent<GameController>().GetPieceStructure();

        playerPiece = BoardGenerator.SpawnPlayer(pieces, GameplayStatistics.Instance.PlayerCount);
    }

    /// <summary>
    /// Called when the player is registered with the game
    /// </summary>
    public virtual void OnRegistered()
    {
        button0Name = GetButtonLongName("Button0");
        button1Name = GetButtonLongName("Button1");
        button2Name = GetButtonLongName("Button2");
        button3Name = GetButtonLongName("Button3");
        button4Name = GetButtonLongName("Button4");
        button5Name = GetButtonLongName("Button5");

        horizontalAxis = GetButtonLongName("Horizontal");
        verticalAxis = GetButtonLongName("Vertical");
    }

    /// <summary>
    /// Called when the player is unregistered with the game
    /// </summary>
    public virtual void OnUnregistered()
    {
        
    }

    /// <summary>
    /// Called every frame that the player can take an action
    /// </summary>
    /// <returns>True if the player has taken there action, false otherwise</returns>
    public virtual bool TakeAction(ActionStatus action)
    {
        EActionDirection direction = GetAxisDirection();
        if(Input.GetButtonDown(button0Name))
        {
            Debug.Log("Move");
            action.ActionType = new MoveAction(direction);
            action.Direction = direction;
            return true;
        }
        else if(Input.GetButtonDown(button1Name))
        {
            Debug.Log("Strong");
            action.ActionType = new StrongAction(direction);
            action.Direction = direction;
            return true;
        }
        else if(Input.GetButtonDown(button2Name))
        {
            action.ActionType = new WideAction(direction);
            action.Direction = direction;
            return true;
        }
        else if(Input.GetButtonDown(button3Name))
        {
            action.ActionType = new LungeAction(direction);
            action.Direction = direction;
            return true;
        }
        else if (Input.GetButtonDown(button4Name))
        {
            action.ActionType = new ItemAction(direction, 1);
            action.Direction = direction;
            return true;
        }
        else if (Input.GetButtonDown(button4Name))
        {
            action.ActionType = new ItemAction(direction, 0);
            action.Direction = direction;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Called every frame the player can take a reaction.
    /// </summary>
    /// <returns>True when the player has finished the reaction.</returns>
    public virtual bool TakeReAction(ReActionStatus action)
    {
        EActionDirection direction = GetAxisDirection();
        if (Input.GetButtonDown(button0Name))
        {
            action.ReactionType = new MoveReaction(direction);
            action.Direction = direction;
            return true;
        }
        else if (Input.GetButtonDown(button1Name))
        {
        }
        else if (Input.GetButtonDown(button2Name))
        {
        }
        else if (Input.GetButtonDown(button3Name))
        {
        }
        else if (Input.GetButtonDown(button4Name))
        {
            action.ReactionType = new ItemReaction(direction, 1);
            action.Direction = direction;
            return true;
        }
        else if (Input.GetButtonDown(button5Name))
        {
            action.ReactionType = new ItemReaction(direction, 0);
            action.Direction = direction;
            return true;
        }

        return false;
    }

    protected string GetButtonLongName(string buttonShortName)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("Joystick");
        sb.Append(index);
        sb.Append(buttonShortName);

        return sb.ToString();
    }

    /// <summary>
    /// Getter for the player piece
    /// </summary>
    /// <returns></returns>
    public Player GetPlayerPiece()
    {
        return playerPiece;
    }

    private EActionDirection GetAxisDirection()
    {
        Vector2 axis = new Vector2();
        axis.x = Input.GetAxisRaw(horizontalAxis);
        axis.y = Input.GetAxisRaw(verticalAxis);

        if (Mathf.Abs(axis.x) > Mathf.Abs(axis.y))
        {
            axis.x = Mathf.Sign(axis.x);
            axis.y = 0;
        }
        else
        {
            axis.y = Mathf.Sign(axis.y);
            axis.x = 0;
        }

        float xDot = Vector2.Dot(axis, Vector2.right);
        float yDot = Vector2.Dot(axis, Vector2.up);

        if(Mathf.Approximately(xDot, 1f))
        {
            return EActionDirection.Right;
        }
        else if(Mathf.Approximately(xDot, -1f))
        {
            return EActionDirection.Left;
        }
        else if(Mathf.Approximately(yDot, 1f))
        {
            return EActionDirection.Up;
        }
        else
            return EActionDirection.Down;
    }

    private int index = -1;


    //Hold a reference to the player piece
    Player playerPiece;

    private bool bSkipAction;
    private bool bSkipReAction;

    private string horizontalAxis;
    private string verticalAxis;
    private string button0Name;
    private string button1Name;
    private string button2Name;
    private string button3Name;
    private string button4Name;
    private string button5Name;

    private int health;
    private int maxHealth;
}
