using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip hitSound;

    private float radius;     
    private Vector2 direction; 
    AudioSource audioData;
    

    void Start()
    {
        
        direction = Vector2.one.normalized;

        radius = transform.localScale.x / 2;

        audioData = GetComponent<AudioSource>();
        
    }

    void Update()
    {

        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y < GameManager.bottomLeft.y + radius && direction.y < 0)
        {
            direction.y = -direction.y;
        }
        if (transform.position.y > GameManager.topRight.y - radius && direction.y > 0)
        {
            direction.y = -direction.y;
        }

        if (transform.position.x < GameManager.bottomLeft.x + radius && direction.x < 0)
        {
            Debug.Log("Oikea pelaaja voitti! (Правый игрок выиграл!)");
            Time.timeScale = 0;
            enabled = false; // lopetetaan skriptin suoritus
        }
        
        else if (transform.position.x > GameManager.topRight.x - radius && direction.x > 0)
        {
            Debug.Log("Vasen pelaaja voitti!");
            Time.timeScale = 0;
            enabled = false; // lopetetaan skriptin suoritus
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Paddle")
        {
            
            bool isRight = other.GetComponent<Paddle>().isRight;

            if (isRight == true && direction.x > 0)
            {
                direction.x = -direction.x;

                audioData.PlayOneShot(hitSound);
                Debug.Log("sound plays");

                speed++;
            }
           
            if (isRight == false && direction.x < 0)
            {
                direction.x = -direction.x;
                
                audioData.PlayOneShot(hitSound);
                Debug.Log("sound plays");

                speed++;
            }
        }
    }
}
