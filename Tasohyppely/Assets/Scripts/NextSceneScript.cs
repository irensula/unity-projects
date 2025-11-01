using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NextSceneScript : MonoBehaviour
{
    public TextMeshProUGUI txtScore;
    // Start is called before the first frame update
    void Start() 
    {
        int score = PlayerPrefs.GetInt("PlayerScore");
        txtScore.text = "Pisteet: " + score;
        GameObject.Find("Player").GetComponent<PlayerController>().score = score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
