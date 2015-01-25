using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    //Size of the inventory
    const int SIZE = 2;

    //A queue of items. 
    List<IItem> items;

    /// <summary>
    /// Constructor
    /// </summary>
    public Inventory()
    {
        items = new List<IItem>();
        for(int i = 0; i < SIZE; i++)
        {
            items.Add(null);
        }
    }

    /// <summary>
    /// When an item is picked up, add it to the inventory
    /// </summary>
    /// <param name="newItem"></param>
    public void AddItem(IItem newItem)
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
    public IItem UseItemAtIndex(int index)
    {
        //If we don't have the specified item, return nothing
        if(index >= items.Count)
        {
            return null;
        }

        IItem item = items[index];
        if (item == null)
            return null;

        items.RemoveAt(index);
        return item;
    }

    public IItem GetItem(int index)
    {
        if (index >= items.Count)
            return null;

        return items[index];
    }
}
