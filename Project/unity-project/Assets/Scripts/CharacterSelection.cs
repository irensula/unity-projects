using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{    
    public CharacterType characterType;
    public float delayBeforeLoad = 3f;

    private Animator anim;
    

    public enum CharacterType
    {
        Boy,
        Girl
    }

    void Start()
    {   
        anim = GetComponent<Animator>();

        int character = PlayerPrefs.GetInt("Character", 0);

        bool isGirl = character == (int)CharacterType.Girl;
        
        anim.SetBool("isGirl", isGirl);
        anim.SetBool("isWalking", false);
    }

    public void OnMouseDown()
    {
        PlayerPrefs.SetInt("Character", (int)characterType);
        StartCoroutine(LoadSceneAfterDelay()); 
    }      

    private IEnumerator LoadSceneAfterDelay()
    {   
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("Scene1");
    } 
}
