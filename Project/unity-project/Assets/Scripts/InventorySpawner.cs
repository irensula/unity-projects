using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    public GameObject[] inventoryToSpawn;
    public Transform[] spawnPoints;
    
    public void SpawnInventoryItems()
    {
        if (inventoryToSpawn.Length != spawnPoints.Length)
        {
            Debug.LogWarning("InventorySpawner: inventoryToSpawn and spawnPoints count do not match!");
            return;
        }

        for (int i = 0; i < inventoryToSpawn.Length && i < spawnPoints.Length; i++)
        {
            Instantiate(inventoryToSpawn[i], spawnPoints[i].position, Quaternion.identity);
        }
    } 
}
