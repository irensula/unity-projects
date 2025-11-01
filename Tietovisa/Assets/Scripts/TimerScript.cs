using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System; 
using TMPro;

public class TimerScript : MonoBehaviour
{
    public TMP_Text txtTime;
    public float currentTime = 0f;
    public float startingTime = 30f;
    public TietovisaScript TietovisaScript;

void Start()
{
    currentTime = startingTime;
}

void Update()
{
    if (txtTime == null) return;

    currentTime -= 1 * Time.deltaTime;
    txtTime.text = "Time: " + Math.Round(currentTime);

    if (currentTime <= 0) 
    {
        if (TietovisaScript != null)
        {
            TietovisaScript.NewQuestion();
        }
    }
}
}
