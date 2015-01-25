using UnityEngine;
using System.Collections;
using System.IO;

public class WideAction : AbsAction, IAction {

    //Statistics
    const int DAMAGE = 1;


    public WideAction(EActionDirection direction)
        : base(direction)
    {
    }

	public EActionType GetActionType()
    {
        return EActionType.Wide;
    }

    public void ReSolve(ReActionStatus reaction, ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Player enemy = reaction.OwnerPlayer.GetPlayerPiece();

        //Call the reaction PreAction
        reaction.ReactionType.PreAction(reaction, status);

        //Perform calculations to assess the action.
        Vector2 adjacentAttackPosition = GetTargetPosition(attacker.GetPosition(), direction);
        Vector2[] hitSpaces = GetWideTargetPositions(attacker.GetPosition(), direction);    //These are all of the spaces hit by the attack
        bool enemyInFront = (adjacentAttackPosition == enemy.GetPosition());  //The enemy hit by center of the wide attack
        EReActionType enemyReaction = reaction.ReactionType.GetReactionType();

        //If enemy is in the attack range, do damage. The only way to protect is to block.
        foreach(Vector2 space in hitSpaces)
        {
            if(enemy.GetPosition() == space)
            {
                if(enemyReaction != EReActionType.Block)
                {
                    enemy.Damage(DAMAGE);
                    SoundManager.Instance.PlaySound(SoundEffectType.damage);
                }
            }
        }
        //If the reaction was a shield bash, push the players away from each other, but only if the enemy is adjacent
        if (enemyInFront && enemyReaction == EReActionType.Bash)
        {
            BashMovement(reaction, status);
        }
        else if (enemyInFront && enemyReaction == EReActionType.Block)
        {
            SoundManager.Instance.PlaySound(SoundEffectType.block);
        }
    }

    /// <summary>
    /// Given a position of the attacker and a direction, calculate all of the spaces that will be affects by the attack.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    private Vector2[] GetWideTargetPositions(Vector2 position, EActionDirection direction)
    {
        Vector2[] hitSpaces = new Vector2[3];

        //Based on the direction enum, calculate the target position
        switch (direction)
        {
            case EActionDirection.Up:
                hitSpaces[0] = position + new Vector2(-1, 1);
                hitSpaces[1] = position + new Vector2(0, 1);
                hitSpaces[2] = position + new Vector2(1, 1);
                break;
            case EActionDirection.Right:
                hitSpaces[0] = position + new Vector2(1, -1);
                hitSpaces[1] = position + new Vector2(1, 0);
                hitSpaces[2] = position + new Vector2(1, 1);
                break;
            case EActionDirection.Down:
                hitSpaces[0] = position + new Vector2(-1, -1);
                hitSpaces[1] = position + new Vector2(0, -1);
                hitSpaces[2] = position + new Vector2(1, -1);
                break;
            case EActionDirection.Left:
                hitSpaces[0] = position + new Vector2(-1, -1);
                hitSpaces[1] = position + new Vector2(-1, 0);
                hitSpaces[2] = position + new Vector2(-1, 1);
                break;
            default:
                return null;
        }

        //Return the array of affected spaces
        return hitSpaces;
    }

    public Quaternion GetSpriteOrientation(ActionStatus status)
    {
        return GetRotationByDirection(direction);
    }

    public Vector2 GetSpritePosition(ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        Vector2 attackPosition = GetTargetPosition(attacker.GetPosition(), direction);
        return attackPosition;
    }

    /// <summary>
    /// Get the tiles that are affected by this action and can be visually changed.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public Vector2[] GetAffectedTiles(ActionStatus status)
    {
        Player attacker = status.OwnerPlayer.GetPlayerPiece();
        return GetWideTargetPositions(attacker.GetPosition(), direction);
    }

    /// <summary>
    /// Get the color that represents this action.
    /// </summary>
    /// <returns></returns>
    public Color GetActionColor()
    {
        return Color.cyan;
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
