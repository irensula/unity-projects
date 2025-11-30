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
        string selected = PlayerPrefs.GetString("SelectedCharacter", "Boy_Player(Clone)");

        GameObject playerToSpawn = selected == "Girl_Player(Clone)" ? girlPrefab : boyPrefab;
        GameObject player = Instantiate(playerToSpawn, spawnPoint.position, spawnPoint.rotation);
        Camera.main.GetComponent<MainCameraFollow>().player = player.transform;
    }
}
