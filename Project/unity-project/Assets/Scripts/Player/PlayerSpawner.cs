using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject boyPrefab;
    public GameObject girlPrefab;
    public Transform spawnPoint;
    
    void Start()
    {
        int character = PlayerPrefs.GetInt("Character", 0);
        
        GameObject playerToSpawn = character == 1 ? girlPrefab : boyPrefab;
        GameObject player = Instantiate(playerToSpawn, spawnPoint.position, spawnPoint.rotation);
        Camera.main.GetComponent<MainCameraFollow>().player = player.transform;
    }
}
