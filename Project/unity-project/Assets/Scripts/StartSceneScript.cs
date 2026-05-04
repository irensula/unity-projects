using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class StartSceneScript : MonoBehaviour
{
    // play background music
    public void Start()
    {
        AudioManager.Instance.PlayBackgroundMusic(SoundId.Background_Music);
    }

    // go to the Character Selecteion Scene
    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
}
