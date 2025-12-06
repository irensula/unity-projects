using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicHitScript : MonoBehaviour
{
    private Rigidbody rb;
    public PlayerController player;
    public AudioSource audioSource;
    public AudioClip hitClip;
    public GameObject hitEffect;
    public float speed = 10f;
    public float ghostScore = 0;
    public float hitRadius = 0.5f;

    public TextMeshProUGUI ghostScoreText;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        if (audioSource != null && audioSource.clip != null)
            audioSource.Play();

        Destroy(gameObject, 5f);
    }
    // method which makes the projectile move
    public void Shoot(Vector3 direction)
    {
        rb.velocity = direction.normalized * speed;
    }
    // hit is checked
    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius);      
        
        foreach (Collider c in hits)
        {
            if(c.CompareTag("Ghost")) 
            {
                // hit effect
                if(hitEffect != null)
                    Instantiate(hitEffect, transform.position, Quaternion.identity);
                
                // play sound
                if(hitClip != null)
                    AudioSource.PlayClipAtPoint(hitClip, transform.position);
                // add score
                GameUIManager.Instance.AddGhostScore(1);

                // tell the spawner a ghost was defeted
                GhostSpawner spawner = FindObjectOfType<GhostSpawner>();
                if (spawner != null)
                    spawner.OnGhostDefeated();

                Destroy(c.gameObject); // destroy the ghost
                Destroy(gameObject); // destroy the magic hit
                
                break; // stop checking after hitting one ghost
            }
        }
    }
}