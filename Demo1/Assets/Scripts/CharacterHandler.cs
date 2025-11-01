using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterHandler : MonoBehaviour
{
    public TextMeshProUGUI txtViesti;
    private int osumat = 0; 
    
    void Start()
    {
        Debug.Log("Testi");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector3 position = this.transform.position;
            position.x--;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector3 position = this.transform.position;
            position.x++;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector3 position = this.transform.position;
            position.y--;
            this.transform.position = position;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector3 position = this.transform.position;
            position.y++;
            this.transform.position = position;
        }
    }

    void OnCollisionEnter2D(Collision2D colInfo)
    {
        print("Törmäys " + gameObject.name + " ja " + colInfo.collider.name);
        osumat++;  // Increase hits count
        txtViesti.text = "Osumia: " + osumat;  // Update UI text
    }
    
    void OnCollisionStay2D(Collision2D colInfo)
    {
        print(gameObject.name + " ja " + colInfo.collider.name + " törmäävät.");
    }
    
    void OnCollisionExit2D(Collision2D colInfo)
    {
        print(gameObject.name + " ja " + colInfo.collider.name + " eivät törmää enää.");
    }
}
