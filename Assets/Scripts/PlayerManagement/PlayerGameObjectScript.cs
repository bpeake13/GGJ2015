using UnityEngine;
using System.Collections;

public class PlayerGameObjectScript : MonoBehaviour {

    Vector3 targetPosition;
    float slideSpeed = 0.25f;

    /// <summary>
    /// Start sliding the player to the specified position.
    /// </summary>
    public void SlideToTargetPosition(Vector3 target)
    {
        targetPosition = target;
        StartCoroutine("MovePieceToPosition");
    }

    /// <summary>
    /// Over time, move the piece to the 
    /// </summary>
    /// <returns></returns>
    IEnumerator MovePieceToPosition()
    {
        Vector3 startPos = transform.position;
        float i = 0;
        float rate = 1 / slideSpeed;
        while (i < 1.0)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, targetPosition, i);
            yield return null;
        }
    }
}
