using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int itemsToWin = 5;
    public event Action OnLevelCompleted;

    public List<string> inventory = new List<string>(); // inventory list

    void Awake()
    {
        if (Instance == null) // singleton list
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keeps GameManager alive between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // add item to the inventory panel
    public void AddItem(string itemId)
    {
        if (!inventory.Contains(itemId))
        {
            inventory.Add(itemId);
            Debug.Log("Item added: " + itemId);
        }

        if (inventory.Count >= itemsToWin)
        {
            LevelCompleted();
        }
    }

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
// inventory items (key, map, flashlight)
// puzzle progress
// unlocked doors
// which panels were already shown
// persistent variables between scenes
// saving progress when game closes