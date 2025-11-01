using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour

{
    Rigidbody2D rigidbody2d;
    public PlayerController player;
    public AudioSource audioSource;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }
    // method which makes the projectile move
    public void Shoot(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
        if (audioSource != null && audioSource.clip != null)
        //     audioSource.PlayOneShot(audioSource.clip);
        audioSource.Play();
    }
    // hit is checked
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return; // only skip if there's no collider

        if(other.CompareTag("Invader")) 
        {
            if (player != null) 
            {
                player.AddScore(1);
            }

            if (audioSource != null && audioSource.clip != null)
                //AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                audioSource.Play();

            Destroy(other.gameObject); // destroy the invader
            Destroy(gameObject);       // destroy the laser
        }
    }    
}
