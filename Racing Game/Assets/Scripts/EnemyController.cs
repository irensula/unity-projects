using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : MonoBehaviour

{
    private GameController gameController;
   
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * gameController.enemySpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
        gameController.HandleCollision(gameObject, col.gameObject);    
    }
}
