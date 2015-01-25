using UnityEngine;
using System.Collections;
using System.IO;

public class MoveAction : AbsAction, IAction
{


    public MoveAction(EActionDirection direction)
        : base(direction)
    {
    }

    public EActionType GetActionType()
    {
        return EActionType.Move;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

        //Perform calculations to assess the action.
        Vector2 targetPosition = GetTargetPosition(attacker.GetPosition(), direction);
        bool enemyInFront = (targetPosition == enemy.GetPosition());  //The enemy is in the way
        EReActionType enemyReaction = reaction.ReactionType.GetReactionType();

        //If you move into a spot dodge, swap the player positions
        if (enemyInFront && enemyReaction == EReActionType.Spot)
        {
            //Swap positions
            GameController.Instance.GetPieceStructure().SwapPiecePositions((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                    (int)targetPosition.x, (int)targetPosition.y);
            SoundManager.Instance.PlaySound(SoundEffectType.dodge);
        }
        //If nothing else is blocking the attacker, move him.
        else
        {
            if (GameController.Instance.GetPieceStructure().isSpaceMovable((int)targetPosition.x, (int)targetPosition.y))
            {
                //Move the player
                GameController.Instance.GetPieceStructure().MovePiece((int)attacker.GetPosition().x, (int)attacker.GetPosition().y,
                                                                        (int)targetPosition.x, (int)targetPosition.y);
                SoundManager.Instance.PlaySound(SoundEffectType.dodge);
            }
            //If the move was invalid, play a sound
            else
            {
                SoundManager.Instance.PlaySound(SoundEffectType.invalid_move);
            }
        }
    }

    public Quaternion GetSpriteOrientation(ActionStatus status)
    {
        return GetRotationByDirection(direction);
    }

    public Vector2 GetSpritePosition(ActionStatus status)
    {
        return status.OwnerPlayer.GetPlayerPiece().GetPosition();
    }

    /// <summary>
    /// Get the tiles that are affected by this action and can be visually changed.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public Vector2[] GetAffectedTiles(ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Vector2 firstStep = GetTargetPosition(attacker.GetPosition(), direction);

        return new Vector2[] {
            firstStep
        };
    }

    /// <summary>
    /// Get the color that represents this action.
    /// </summary>
    /// <returns></returns>
    public Color GetActionColor()
    {
        return Color.blue;
	}
	
    public void Serialize(BinaryWriter writer)
    {
        writer.Write((char)direction);
    }

    public void Deserialize(BinaryReader reader)
    {
        this.direction = (EActionDirection)reader.ReadChar();
    }
}
