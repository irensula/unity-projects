using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{    
    public CharacterType characterType;
    public float delayBeforeLoad = 3f;

    private Animator anim;
    public Animator boyAnim;
    public Animator girlAnim;

    public enum CharacterType
    {
        Boy,
        Girl
    }

    void Start()
    {   
        anim = GetComponentInChildren<Animator>();

        int character = PlayerPrefs.GetInt("Character", 0);
        boyAnim.SetFloat("Character", 0);
        girlAnim.SetFloat("Character", 1);

        bool isGirl = character == (int)CharacterType.Girl;
        
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
