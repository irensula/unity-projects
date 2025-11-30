using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class StartSceneScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
