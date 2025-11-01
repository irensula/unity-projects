using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    // a reference to the player
    public PlayerController player;
    
    // Update is called once per frame
    void Update()
    {
        // the invader continuously moves straight down at a speed of 1 unit/sec
        transform.Translate(Vector2.down * GameData.invaderSpeed * Time.deltaTime, Space.World);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
