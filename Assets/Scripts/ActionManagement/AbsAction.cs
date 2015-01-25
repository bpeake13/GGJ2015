using UnityEngine;
using System.Collections;

public abstract class AbsAction {


    //Keep track of the direction of this attack
    protected EActionDirection direction;

    public AbsAction(EActionDirection direction)
    {
        this.direction = direction;
    }

    /// <summary>
    /// Getter for the direction
    /// </summary>
    /// <returns></returns>
    public EActionDirection GetDirection()
    {
        return direction;
    }

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
                offset = new Vector2(0, -1);
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
    /// Get the Quaternion rotation of the sprite to draw based on the direction of the action.
    /// </summary>
    /// <returns></returns>
    protected Quaternion GetRotationByDirection(EActionDirection direction)
    {
        switch (direction)
        {
            case EActionDirection.Up:
                return Quaternion.Euler(90, 270, 0);
            case EActionDirection.Right:
                return Quaternion.Euler(90, 0, 0);
            case EActionDirection.Down:
                return Quaternion.Euler(90, 90, 0);
            case EActionDirection.Left:
                return Quaternion.Euler(90, 180, 0);
            default:
                return Quaternion.identity;
        }
    }

    /// <summary>
    /// Given a position and direction, return the spot opposite of the specified direction
    /// </summary>
    /// <param name="position"></param>
    /// <param name="directionToReverse"></param>
    /// <returns></returns>
    protected Vector2 GetTargetPositionOppositeDirection(Vector2 position, EActionDirection directionToReverse)
    {
        switch (direction)
        {
            case EActionDirection.Up:
                return GetTargetPosition(position, EActionDirection.Down);
            case EActionDirection.Right:
                return GetTargetPosition(position, EActionDirection.Left);
            case EActionDirection.Down:
                return GetTargetPosition(position, EActionDirection.Up);
            case EActionDirection.Left:
                return GetTargetPosition(position, EActionDirection.Right);
            default:
                return Vector2.zero;
        }
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

    /// <summary>
    /// If the reaction is to shield bash and the reaction was a success, call this function to handle the movement.
    /// </summary>
    /// <param name="reaction"></param>
    /// <param name="status"></param>
    protected void BashMovement(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Move the enemy in the direction the attacker hit.
        //Push the enemy back a space.
        Vector2 pushBackSpaceEnemy = GetTargetPosition(enemy.GetPosition(), direction);
        if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)pushBackSpaceEnemy.x, (int)pushBackSpaceEnemy.y))//Only push them back if there is no wall there.
        {
            GameController.Instance.GetPieceStructure().MovePiece((int)enemy.GetPosition().x, (int)enemy.GetPosition().y,
                                                                (int)pushBackSpaceEnemy.x, (int)pushBackSpaceEnemy.y);
        }

        //Push the attacker back a space.
        Vector2 pushBackSpaceAttacker = GetTargetPositionOppositeDirection(attacker.GetPosition(), direction);
        if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)pushBackSpaceAttacker.x, (int)pushBackSpaceAttacker.y))//Only push them back if there is no wall there.
        {
            GameController.Instance.GetPieceStructure().MovePiece((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                (int)pushBackSpaceAttacker.x, (int)pushBackSpaceAttacker.y);
        }

        SoundManager.Instance.PlaySound(SoundEffectType.block);
    }
}
