using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public TextMeshProUGUI txtScore;

    void Start() 
    {
        int score = PlayerPrefs.GetInt("PlayerScore");
        txtScore.text = "Pisteet: " + score;
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene("SceneGame");
    }

}
