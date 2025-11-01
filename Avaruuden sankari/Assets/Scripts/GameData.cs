using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static string playerName;
    
    public static int playerScore = 0;   
    public static int playerHealth = 5;
    public static int currentLevel = 1;
    
    public static float playerSpeed = 5f; 
    public static float invaderSpeed = 2f;
    public static float spawnInterval = 3f;
}
