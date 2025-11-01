using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class StartSceneScript : MonoBehaviour
{
    public TMP_InputField txtInputName;
    
    public void StartGame()
    {
        GameData.playerName = txtInputName.text;

        GameData.playerScore = 0;
        GameData.playerHealth = 5;
        GameData.currentLevel = 1;
        GameData.playerSpeed = 5f;
        GameData.invaderSpeed = 2f;
        GameData.spawnInterval = 3f;
        
        SceneManager.LoadScene("GameScene");
    }
}
