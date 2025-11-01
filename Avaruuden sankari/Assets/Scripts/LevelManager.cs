using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManagerManager : MonoBehaviour
{
    public TextMeshProUGUI healthText; // ui text for the health (power)
    public TextMeshProUGUI scoreText; // ui text for the score (score)
    // Start is called before the first frame update
    void Start()
    {
        // when the player is on the 2 level, his speed increeses, invaders' speed increases, spawn interval decreases
        if (GameData.currentLevel == 2) 
        {
            GameData.playerSpeed = Mathf.Min(GameData.playerSpeed + 1f, 10f);

            GameData.invaderSpeed = Mathf.Min(GameData.invaderSpeed + 1f, 10f);

            // the speed will not become less than 0.5f
            GameData.spawnInterval = Mathf.Max(GameData.spawnInterval - 1, 1f);

            // updates health and score UI in the new scene
            if (healthText != null) 
            {
                healthText.text = "Health: " + GameData.playerHealth;
            }
            if (scoreText != null) 
            {
                scoreText.text = "Score: " + GameData.playerScore;
            }
        }
        // when the player is on the 3 level, his speed increeses, invaders' speed increases, spawn interval decreases
        if (GameData.currentLevel == 3) 
        {
            GameData.playerSpeed = Mathf.Min(GameData.playerSpeed + 1f, 10f);

            GameData.invaderSpeed = Mathf.Min(GameData.invaderSpeed + 1f, 10f);

            // the speed will not become less than 0.5f
            GameData.spawnInterval = Mathf.Min(GameData.spawnInterval - 1, 2f);

            // updates health and score UI in the new scene
            if (healthText != null) 
            {
                healthText.text = "Health: " + GameData.playerHealth;
            }
            if (scoreText != null) 
            {
                scoreText.text = "Score: " + GameData.playerScore;
            }
        }
    }
}
