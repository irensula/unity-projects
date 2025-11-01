using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{
    public GameObject invaderPrefab; // assign prefab in Inspector
    public float spawnInterval = 3f; // seconds between spawns
    public float spawnRangeX = 10f; // width range (adjust for the screen)
    public PlayerController player; // reference to the player
    float topY; // top of the screen
    
    // Start is called before the first frame update
    void Start()
    {
        topY = Camera.main.orthographicSize; // top of the screen
        InvokeRepeating("SpawnInvader", 1f, GameData.spawnInterval); // calls method firstly afetr 1 second, then with spawnInterval
    }
    
    void SpawnInvader() // method which spawns invaders
    {
        if (player == null) return; // checks if there is a reference to the player

        float randomX = Random.Range(-spawnRangeX, spawnRangeX); // chose the random position in the spawnRangeX
        Vector2 spawnPos = new Vector2(randomX, topY + 1f); // coordinates of the invader: randomX and topY

        // creates a new invader with new random coordinates, rotates it
        GameObject invader = Instantiate(invaderPrefab, spawnPos, invaderPrefab.transform.rotation);  

        Invader invaderScript = invader.GetComponent<Invader>(); // gives a script to a new invader
        
        if (invaderScript != null) // check if invaderScript exists
        {
            invaderScript.player = player; // assign the existing player reference to the invader
        }
    }
}
