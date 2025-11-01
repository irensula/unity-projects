using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI txtPaused;
    private bool paused;
    public TextMeshProUGUI txtScore;
    public int playerScore = 0;
    private EnemyController enemyController;
    public CarController carController;
    private EnemySpawner enemySpawner;
    private bool levelUp1 = false;
    private bool levelUp2 = false;
    public float enemySpeed = 0.5f;
   
    void Start()
    {
        carController = FindObjectOfType<CarController>();
        enemyController = FindObjectOfType<EnemyController>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
        if (paused) 
        {
            paused = false;
            txtPaused.text = "";
            Time.timeScale = 1;	
            Debug.Log("Game resumed");
        }
        else 
        {
            paused = true;
            txtPaused.text = "PAUSE";
            Time.timeScale = 0;	
            Debug.Log("The space is pressed");
        }
        }
        LevelUp1();
        LevelUp2();
    }

    public void HandleCollision(GameObject enemy, GameObject collider) {
    if (collider.gameObject.CompareTag("destroyer")) 
        {
            playerScore++;
            txtScore.text = "Score: " + playerScore;
            Destroy(enemy);
        }
    }

    void LevelUp1()
    {
        if (playerScore >= 5 && !levelUp1) 
        {
            levelUp1 = true;
            
            carController.speed++; 
            enemySpawner.delayTimer--;
            enemySpeed++;

            TrackController track = GameObject.Find("Track").GetComponent<TrackController>();
            track.AddSpeed();
            
            Debug.Log("Car's speed: " + carController.speed + " Delay timer" + enemySpawner.delayTimer + " Enemy's speed" + enemySpeed);
        }
    }
    void LevelUp2()
    {
        if (playerScore >= 10 && !levelUp2) 
        {
            levelUp2 = true;
            
            carController.speed++; 
            enemySpawner.delayTimer--;
            enemySpeed++;

            TrackController track = GameObject.Find("Track").GetComponent<TrackController>();
            track.AddSpeed();
            
            Debug.Log("Car's speed: " + carController.speed + " Delay timer" + enemySpawner.delayTimer + " Enemy's speed" + enemySpeed);
        }
    }
}
