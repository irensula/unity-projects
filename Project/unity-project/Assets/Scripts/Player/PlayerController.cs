using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;
    private Rigidbody rb;

    public GameObject magicPrefab;
    public Transform firePoint; 
    public float fireCooldown = 0.5f;
    private float nextFireTime = 0f;

    public PlayerHealthUI healthUI;  
    public AudioClip hitClip;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (healthUI == null)
        {
            healthUI = FindObjectOfType<PlayerHealthUI>();
            if (healthUI == null)
                Debug.LogError("PlayerHealthUI not found in the scene!");
        }

        // anim = GetComponent<Animator>();
        anim = GetComponentInChildren<Animator>();
        int character = PlayerPrefs.GetInt("Character", 0);
        anim.SetFloat("Character", character);

        if (anim == null)
        {
            Debug.LogError("Animator NOT FOUND!");
        }
        else
        {
            Debug.Log("Animator found on: " + anim.gameObject.name);
        }
    }

    // player's moves
    void FixedUpdate()
    {
        float move = 0f;
        float rotation = 0f;

        // move forward/backward
        if (Input.GetKey(KeyCode.UpArrow))
            move = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            move = -1f;

        // rotate left/right
        if (Input.GetKey(KeyCode.LeftArrow))
            rotation = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            rotation = 1f;

        // apply rotation
        transform.Rotate(0f, rotation * rotationSpeed * Time.fixedDeltaTime, 0f);

        // move in the facing direction
        Vector3 forwardMovement = transform.forward * move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);

        // shooting input
        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFireTime)
        {
            ShootMagic();
            nextFireTime = Time.time + fireCooldown;
        }

        anim.SetBool("isWalking", move != 0);
        Debug.Log("move = " + move + " | isWalking = " + (move != 0));
    }

    // magic shoot
    void ShootMagic()
    {
        GameObject magic = Instantiate(magicPrefab, firePoint.position, firePoint.rotation);
        
        MagicHitScript magicScript = magic.GetComponent<MagicHitScript>();
        
        if (magicScript != null)
        {
            magicScript.player = this;   
            magicScript.Shoot(firePoint.forward);

            SoundEvents.Magic.Hit(); // magic hit sound
        }
    }

    // on collider with ghosts, coins, diamonds
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            healthUI.TakeDamage(1);
            GhostSpawner spawner = FindObjectOfType<GhostSpawner>();
            if (spawner != null)
            {
                spawner.OnGhostDefeated();
            }
            Destroy(other.gameObject);
            CheckGameOver();
        }
        if (other.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            GameUIManager.Instance.AddCoinScore(1);
        }
        if (other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            GameUIManager.Instance.AddDiamondScore(1);
        }
    }

    void CheckGameOver()
    {
        if (healthUI.currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
