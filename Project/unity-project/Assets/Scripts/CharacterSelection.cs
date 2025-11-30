using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class CharacterSelection : MonoBehaviour
{    
    public string characterName;
    public float delayBeforeLoad = 3f;

    public void OnMouseDown()
    {
        PlayerPrefs.SetString("SelectedCharacter", characterName);
        Debug.Log(characterName + " is selected!");

        StartCoroutine(LoadSceneAfterDelay()); 
    }      

    private System.Collections.IEnumerator LoadSceneAfterDelay()
    {   
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("Scene1");
    } 
}
