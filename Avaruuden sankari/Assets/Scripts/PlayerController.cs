using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction; // player movement input (arrow keys)
    Rigidbody2D rigidbody2d; // physics component used to move the player
    Vector2 move; // stores the input vector from player controls 
    public float maxHealth = 5;

    public TextMeshProUGUI healthText; // ui text for the health (power)
    public TextMeshProUGUI scoreText; // ui text for the score (score)
    public TextMeshProUGUI levelCompleteText;

    public GameObject laserPrefab;

    void Start()
    {
        MoveAction.Enable(); // turns on input so Unity reads player controls
        rigidbody2d = GetComponent<Rigidbody2D>(); // gets the Rigidbody2D component attached to the player (used for physics movement)
        UpdateHealthText(); // Updates the health UI text
        UpdateScoreText(); // Updates the score UI text
    }

    void FixedUpdate() 
    {
        Vector2 position = (Vector2)rigidbody2d.position + move * GameData.playerSpeed * Time.deltaTime; // calculates new position: current position + movement input × speed × deltaTime
        rigidbody2d.MovePosition(position); // moves the player smoothly using physics, instead of changing Transform directly
    }

    // when the player collides with a trigger
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other != null)
        {
            if (other.CompareTag("Invader") || other.CompareTag("Asteroid")) // checks if the collision occurred with the "Invader" or "Asteroid" tag
            {
                GameData.playerHealth -= 1; // decreases the player's health by 1.
                UpdateHealthText(); // updates the player's health
                Destroy(other.gameObject); // destroys the object the player collided with (Invader or Asteroid)

                CheckGameOver(); // calls the method to check if the game over
            }
        }
    }

    public void AddScore(int amount) 
    {
        GameData.playerScore += amount; // increases the player's score by amount
        UpdateScoreText(); // updates the score text UI

        CheckGameOver(); // calls the method to check if the game over
        
        if (GameData.playerScore >= 10 && GameData.currentLevel == 1) // if the score reaches the thresholds 10
        {
            GameData.currentLevel = 2; // go to the second level (SecondScene)

            if (!levelCompleteText.gameObject.activeSelf)
            {
                StartCoroutine(ShowLevelCompleteText("SecondScene")); // shows the text for 3 seconds, then loads a new scene.
            }
        }

        if (GameData.playerScore >= 50 && GameData.currentLevel == 2) // if the score reaches the thresholds 50
        {
            GameData.currentLevel = 3; // go to the third level (ThirdScene)

            if (!levelCompleteText.gameObject.activeSelf)
            {
                StartCoroutine(ShowLevelCompleteText("ThirdScene")); // shows the text for 3 seconds, then loads a new scene
            }
        }
    }

    // A coroutine in Unity is a special method that can be paused and resumed after a certain amount of time or when a condition is met.
    private IEnumerator ShowLevelCompleteText(string nextScene)
    {
        levelCompleteText.gameObject.SetActive(true); // makes the text visible (SetActive(true))
        yield return new WaitForSeconds(3f); // the pause
        levelCompleteText.gameObject.SetActive(false); // hides the text and loads the next scene

        SceneManager.LoadScene(nextScene); // changes the scene
    }

    public void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + GameData.playerHealth; // updates health text UI on the screen
        }
    }

    public void UpdateScoreText()
    {
        if (scoreText != null) {
            scoreText.text = "Score: " + GameData.playerScore; // updates score text UI on the screen
        }
    }

    void Shoot()
    {
        GameObject laser = Instantiate(laserPrefab, rigidbody2d.position + Vector2.up * 1.5f, Quaternion.identity); // creates a laser above the player (+1.5 in Y)

        LaserManager projectile = laser.GetComponent<LaserManager>(); // calls the Shoot() method on the LaserManager to make the laser move upwards
        projectile.Shoot(new Vector2(0,1), 0.2f);
        projectile.player = this; // passes a reference to the player, so the laser can interact with the player or the UI
    }

    void Update()
    {
        move = MoveAction.ReadValue<Vector2>(); // reads player movement every frame

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); // checks if space-key is pressed - if so, fires a laser.
        }
    }

    void CheckGameOver() 
    // checks the game over conditions
    {
        if (GameData.playerHealth <= 0) // if health <= 0 - loss
        {
            SceneManager.LoadScene("GameOver"); // loads the GameOver scene
        }
        if (GameData.playerScore >= 200) // if score >= 100 - win
        {
            SceneManager.LoadScene("GameOver"); // loads the GameOver scene
        }
    }
}
