using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    //Size of the inventory
    const int SIZE = 2;

    //A queue of items. 
    List<IItem> items;

    /// <summary>
    /// When an item is picked up, add it to the inventory
    /// </summary>
    /// <param name="newItem"></param>
    public void PickUpItem(IItem newItem)
    {
        items.Add(newItem);

        //Remove the last item in the queue if it's full
        if(items.Count > SIZE)
        {
            items.RemoveAt(0);
        }
    }

    /// <summary>
    /// Allow the player to get access to any item in the inventory.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public IItem GetItemByIndex(int index)
    {
        IItem item = items[index];
        items.RemoveAt(0);
        return item;
    }
}
