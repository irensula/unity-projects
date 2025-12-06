using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnInterval = 5f;
    public float speed = 3f;
    public float spawnDistance = 10f;
    public int amount = 15;
    public float minDistanceFromPlayer = 5f;

    public Transform player;

    public GameObject congratsGhostPanel;
    
    private int spawnedCount = 0;
    private int defeatedGhosts = 0;
    private bool spawningStarted = false;

    void Start()
    {
        StartCoroutine(FindPlayerAndStart());
    }

    IEnumerator FindPlayerAndStart() 
    {
        // wait for the player
        while (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
            {
                player = p.transform;
                break;
            }
            yield return new WaitForSeconds(0.2f);
        }

        if (player == null)
        {
            Debug.LogError("Player was not found in the scene");
            yield break;
        }
        if (!spawningStarted)
        {
            spawningStarted = true;
            StartCoroutine(SpawnGhostsOverTime());
        }
    }

    IEnumerator SpawnGhostsOverTime()
    {
        while (spawnedCount < amount)
        {
            SpawnGhost();
            spawnedCount++;
            Debug.Log($"GhostSpawner: создано {spawnedCount}/{amount}");
            yield return new WaitForSeconds(spawnInterval);
        }
        Debug.Log($"GhostSpawner: all {amount} of ghosts were spawned");
    }
   
    Vector3 GetSpawnPositionOutsideCamera()
    {
        // получаем камеру
        Camera cam = Camera.main;
        float camDistance = 10f; // на сколько далеко от игрока спавним
        float spawnHeight = 1f;

        Vector3 spawnPos = Vector3.zero;

        int side = Random.Range(0, 4); // 0 = left, 1 = right, 2 = top, 3 = bottom
        switch (side)
        {
            case 0: // left
                spawnPos = player.position + Vector3.left * spawnDistance + Vector3.forward * Random.Range(-5f, 5f);
                break;
            case 1: // right
                spawnPos = player.position + Vector3.right * spawnDistance + Vector3.forward * Random.Range(-5f, 5f);
                break;
            case 2: // top
                spawnPos = player.position + Vector3.forward * spawnDistance + Vector3.right * Random.Range(-5f, 5f);
                break;
            case 3: // bottom
                spawnPos = player.position + Vector3.back * spawnDistance + Vector3.right * Random.Range(-5f, 5f);
                break;
        }

    spawnPos.y = spawnHeight;

    // проверка земли
    RaycastHit hit;
    if (Physics.Raycast(spawnPos + Vector3.up * 10f, Vector3.down, out hit, 20f))
    {
        spawnPos.y = hit.point.y + 0.5f;
    }

    return spawnPos;
}

    void SpawnGhost()
    {
        Vector3 spawnPos = GetSpawnPositionOutsideCamera();
    GameObject ghost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);

    
    GhostController ghostScript = ghost.GetComponent<GhostController>();
    if (ghostScript != null && player != null)
    {
        ghostScript.Init(player);   
        ghostScript.speed = speed;  
    }
    }

    public void OnGhostDefeated()
    {
        defeatedGhosts++;

        if (defeatedGhosts >= amount)
        {
            Debug.Log("All ghosts were defeated!");
            GameUIManager.Instance.ShowCongratsPanel(GameUIManager.Instance.congratsGhostPanel);
            GameUIManager.Instance.ghostPanelShown = true;
            Debug.Log("ghostPanelShown" + GameUIManager.Instance.ghostPanelShown);
        }
    }
}
