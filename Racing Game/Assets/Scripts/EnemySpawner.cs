using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float timer;
    public float delayTimer;
    public GameObject[] enemies;
    

    void Start()
    {
        // float posX = Random.Range(leftRange, rightRange);
        float posX = Random.Range(-5.0f, 5.0f);
        Vector3 carPos = new Vector3(posX, transform.position.y, 0);
        int carIndex = Random.Range(0,5);
        Instantiate(enemies[carIndex], carPos, transform.rotation);
        
        timer = delayTimer;
    }

    void Update()
    {
        timer -= Time.deltaTime;
	    if (timer < 0) 
        {
            // float posX = Random.Range(leftRange, rightRange);
            float posX = Random.Range(-5.0f, 5.0f);
            int carIndex = Random.Range(0,5);
            Vector3 carPos = new Vector3(posX, transform.position.y, 0); 
            Instantiate(enemies[carIndex], carPos, transform.rotation);
            timer = delayTimer;
        }
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemySpawner : MonoBehaviour
// {
//     public GameObject enemy;
//     public float timer;
//     public float delayTimer;
//     // Start is called before the first frame update
//     void Start()
//     {
//         float x = Random.Range(-3.0f, 3.0f);
//         Vector3 carPos = new Vector3(x, transform.position.y, 0);
//         Instantiate(enemy, carPos, transform.rotation);
        
//         timer = delayTimer;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         timer -= Time.deltaTime;
// 	    if (timer < 0) 
//         {
// 		// create a new car and reset the timer
//             float x = Random.Range(-3.0f, 3.0f);
// 		    Vector3 carPos = new Vector3(x, transform.position.y, 0);
//             Instantiate(enemy, carPos, transform.rotation); 
//             timer = delayTimer;
//         }
//     }
// }