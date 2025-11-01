using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnInterval = 5f;
    public float speed = 5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 1f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnAsteroid()
    {
        Camera cam = Camera.main;
        
        // Get screen bounds in world units
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        Vector2 spawnPos = Vector2.zero;
        Vector2 targetPos = Vector2.zero;
        
        // Pick a random side: 0=top,1=bottom,2=left,3=right
        int side = Random.Range(0, 4);
        switch (side)
        {
            case 0: // top
                spawnPos = new Vector2(Random.Range(-camWidth/2, camWidth/2), camHeight/2 + 1);
                targetPos = new Vector2(Random.Range(-camWidth/2, camWidth/2), -camHeight/2 - 1);
                break;
            case 1: // bottom
                spawnPos = new Vector2(Random.Range(-camWidth/2, camWidth/2), -camHeight/2 - 1);
                targetPos = new Vector2(Random.Range(-camWidth/2, camWidth/2), camHeight/2 + 1);
                break;
            case 2: // left
                spawnPos = new Vector2(-camWidth/2 - 1, Random.Range(-camHeight/2, camHeight/2));
                targetPos = new Vector2(camWidth/2 + 1, Random.Range(-camHeight/2, camHeight/2));
                break;
            case 3: // right
                spawnPos = new Vector2(camWidth/2 + 1, Random.Range(-camHeight/2, camHeight/2));
                targetPos = new Vector2(-camWidth/2 - 1, Random.Range(-camHeight/2, camHeight/2));
                break;
        }

        // create asteroid
        GameObject asteroid = Instantiate(asteroidPrefab, spawnPos, Quaternion.identity);
        AsteroidController asteroidScript = asteroid.GetComponent<AsteroidController>();
        asteroidScript.Init(targetPos);
        asteroidScript.speed = speed;
    }
}
