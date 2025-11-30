using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject ghostPrefab;
    public float spawnInterval = 5f;
    public float speed = 3f;
    public float spawnDistance = 10f;
    public int amount = 5;
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
   
    void SpawnGhost()
    {
        if (player == null) return;

        Vector3 spawnPos = Vector3.zero;
        Vector3 targetPos = Vector3.zero;

        int side = Random.Range(0,3);
        float offsetZ = Random.Range(-5f, 5f);
        float offsetX = Random.Range(-5f, 5f);
        
        switch (side)
        {
            case 0: // left
                spawnPos = new Vector3(player.position.x - spawnDistance, player.position.y, player.position.z + offsetZ);
                targetPos = new Vector3(player.position.x + spawnDistance, player.position.y, player.position.z + offsetZ);
                break;
            case 2: // right
                spawnPos = new Vector3(player.position.x + spawnDistance, player.position.y, player.position.z + offsetZ);
                targetPos = new Vector3(player.position.x - spawnDistance, player.position.y, player.position.z + offsetZ);
                break;
            case 3: // front
                spawnPos = new Vector3(player.position.x + offsetX, player.position.y, player.position.z + spawnDistance);
                targetPos = new Vector3(player.position.x + offsetX, player.position.y, player.position.z - spawnDistance);
                break;
        }

        // check the distance to the player
        if (Vector3.Distance(spawnPos, player.position) < minDistanceFromPlayer)
        {
            // move the ghost further from the player
            Vector3 dir = (spawnPos - player.position).normalized;
            spawnPos = player.position + dir * minDistanceFromPlayer;
        }

        GameObject ghost = Instantiate(ghostPrefab, spawnPos, Quaternion.identity);
        GhostController ghostScript = ghost.GetComponent<GhostController>();
        
        if (ghostScript != null)
        {
            ghostScript.Init(player);
            ghostScript.speed = speed;
        }
    }

    // show congrats panel when all ghosts were destroyed
    public void OnGhostDefeated()
    {
        defeatedGhosts++; // count destroyed ghosts

        if (defeatedGhosts >= amount) // compare with spawned amount of ghosts
        {
            Debug.Log("All ghosts were defeated!");
            GameUIManager.Instance.ShowCongratsPanel(GameUIManager.Instance.congratsGhostPanel); // show congrats ghost panel
        }
    }
}
