using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject coinPrefab;
    public GameObject diamondPrefab;

    [Header("Spawn points (X, Y, Z)")]
    public List<Vector3> coinSpawnPoints = new List<Vector3>();
    public List<Vector3> diamondSpawnPoints = new List<Vector3>();

    void Start()
    {
        SpawnItemsAtPoints(coinPrefab, coinSpawnPoints);
        SpawnItemsAtPoints(diamondPrefab, diamondSpawnPoints);
    }

    // create coins and diamonds
    void SpawnItemsAtPoints(GameObject prefab, List<Vector3> spawnPoints)
    {
        int index = 0;

        foreach (Vector3 pos in spawnPoints)
        {
            if (prefab != null)
            {
                GameObject newItem = Instantiate(prefab, pos, Quaternion.identity);
                newItem.name = prefab.name + "_" + index;

                MeshRenderer[] newRenderers = newItem.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer mr in newRenderers)
                {
                    if (mr.sharedMaterial != null)
                    {
                        mr.material = new Material(mr.sharedMaterial);
                        mr.material.SetColor("_Color", mr.sharedMaterial.color);
                    }
                }
                index++;
            }
        }
        GameUIManager.Instance.totalCoinsInScene = GameObject.FindGameObjectsWithTag("Coin").Length;
        GameUIManager.Instance.totalDiamondsInScene = GameObject.FindGameObjectsWithTag("Diamond").Length;
    }
}
