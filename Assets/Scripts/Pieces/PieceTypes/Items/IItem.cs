using UnityEngine;
using System.Collections;

public interface IItem : IPiece{

    /// <summary>
    /// Call this function when a player uses this item.
    /// </summary>
    void Activate();
}
