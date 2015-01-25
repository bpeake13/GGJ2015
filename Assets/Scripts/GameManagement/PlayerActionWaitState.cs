using UnityEngine;
using System.Collections;

public class PlayerActionWaitState : GameState
{
    public PlayerActionWaitState(ActionStatus action)
    {
        this.player = action.OwnerPlayer;
        this.action = action;
    }

    public override void Enter()
    {
    }

    public override void Update()
    {
        bool wasAction = player.TakeAction(action);

        if(wasAction)
        {
            //Draw the sprite for the action onto the screen.
            Sprite sprite = GameController.Instance.gameObject.GetComponent<PieceObjectConverter>().GetSpriteByAction(action.ActionType.GetActionType());
            int x = (int)player.GetPlayerPiece().GetPosition().x;
            int y = (int)player.GetPlayerPiece().GetPosition().y;
            GameController.Instance.gameObject.GetComponent<GuiStack>().DrawGrid(sprite, x, y);

            PlayerReactionStartState next = new PlayerReactionStartState(action);
            SwitchState(next);
        }
    }

    public override void Exit()
    {
    }

    private PlayerController player;

    private ActionStatus action;
}
