using UnityEngine;
using System.Collections;

public abstract class AbsAction {


    /// <summary>
    /// Based on my movement direction, get the position I would like to end up.
    /// </summary>
    /// <returns></returns>
    protected Vector2 GetTargetPosition(Vector2 position, EActionDirection direction)
    {
        //Based on the direction enum, calculate the target position
        Vector2 offset;
        switch (direction)
        {
            case EActionDirection.Up:
                offset = new Vector2(0, 1);
                break;
            case EActionDirection.Right:
                offset = new Vector2(1, 0);
                break;
            case EActionDirection.Down:
                offset = new Vector2(0, -11);
                break;
            case EActionDirection.Left:
                offset = new Vector2(-1, 0);
                break;
            default:
                offset = Vector2.zero;
                break;
        }

        //Calculate the spot we want to move to
        return position + offset;
    }

    /// <summary>
    /// Move the enemy if their reaction allows it.
    /// </summary>
    /// <param name="enemyPosition"></param>
    /// <param name="targetPosition"></param>
    /// <returns></returns>
    protected Vector2 EnemyMoveReaction(Vector2 enemyPosition, Vector2 targetPosition)
    {
        //If they can move, move them.
        if(GameController.Instance.GetPieceStructure().isSpaceMovable((int)targetPosition.x, (int)targetPosition.y))
        {
            //Move the actual piece
            GameController.Instance.GetPieceStructure().MovePiece((int)enemyPosition.x, (int)enemyPosition.y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
            return targetPosition;
        }
        //If the space isn't valid anymore, move this player back to their old position.
        return enemyPosition;
    }
}
