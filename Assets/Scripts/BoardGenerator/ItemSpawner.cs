using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner {

    //Keep a reference to the pieces
    PieceStructure pieces;

    //Spawn Timer
    int MIN_TURNS_UNTIL_SPAWN = 8;
    int MAX_TURNS_UNTIL_SPAWN = 15;
    int turnsUntilSpawn;

    //Items to spawn
    PieceType[] items = new PieceType[]
    {
        PieceType.health_potion
    };

    //The percent chance of spawning, out of 100.
    int[] itemSpawnChance = new int[]
    {
        100
    };

    /// <summary>
    /// Constructor. Initialize the item spawner.
    /// </summary>
    public ItemSpawner(PieceStructure pieces)
    {
        //Initialize the number of turns until an item spawns
        SetTurnsUntilSpawn();

        //Keep a reference of the piece structure
        this.pieces = pieces;
    }

    /// <summary>
    /// Reset the turn timer
    /// </summary>
    private void SetTurnsUntilSpawn()
    {
        turnsUntilSpawn = Random.Range(MIN_TURNS_UNTIL_SPAWN, MAX_TURNS_UNTIL_SPAWN);
    }

    /// <summary>
    /// Call this when a turn passes to check if an item needs to spawn.
    /// </summary>
    public void TurnPassed()
    {
        turnsUntilSpawn--;
        //If the turn counter ends, spawn an item
        if(turnsUntilSpawn == 0)
        {
            SpawnItem();
            SetTurnsUntilSpawn();
        }
    }

    /// <summary>
    /// Spawn a new item when it is time. Spawn it at a random position that is valid.
    /// </summary>
    private void SpawnItem()
    {
        int randomNumber = Random.Range(0, 100);

        //Iterate down the list of items and spawn them based on their percentage chance of spawning
        for(int i = 0; i < items.Length; i ++)
        {
            if (randomNumber <= itemSpawnChance[i])
            {
                //Spawn item at a random position
                int x, y;
                do
                {
                    x = Random.Range(0, pieces.GetBoardWidth());
                    y = Random.Range(0, pieces.GetBoardHeight());
                } while (!pieces.isSpaceEmpty(x, y));//Keep looping until we find a valid space to place the item.

                //Actually create the item.
                pieces.CreatePiece(x, y, items[i]);
            }
        }
    }

}
