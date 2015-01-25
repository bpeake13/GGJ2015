using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


class PlaybackShowActionState : GameState
{
    public PlaybackShowActionState(ActionStatus action)
    {
        this.action = action;
    }

    public override void Enter()
    {
        //Draw the sprite for the action onto the screen.
        Sprite sprite = GameController.Instance.gameObject.GetComponent<PieceObjectConverter>().GetSpriteByAction(action.ActionType.GetActionType());
        Vector2 spritePosition = action.ActionType.GetSpritePosition(action);
        int x = (int)spritePosition.x;
        int y = (int)spritePosition.y;
        GameController.Instance.gameObject.GetComponent<GuiStack>().DrawGrid(sprite, x, y, action.ActionType.GetSpriteOrientation(action));

        GameDisplay.Instance.SetPlayerTurn(action.OwnerPlayer);
    }

    public override void Update()
    {
        timer -= Time.unscaledDeltaTime;

        if(timer <= 0)
        {
            SwitchState(new PlaybackResolverState(action));
        }
    }

    public override void Exit()
    {
    }

    private ActionStatus action;

    private float timer = 1f;
}
