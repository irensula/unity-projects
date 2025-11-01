using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class PlayerController : MonoBehaviour
{
    // public float speed;
    public float speed;

    // public float jumpforce;
    public float jumpforce = 20f;
    public float moveInput;
    private Rigidbody2D rb;
    private bool facingRight = true;

    // onko hahmo maassa?
    private bool isGrounded;
    // tarkistus onko maa jalkojen kohdalla
    public Transform groundCheck;
    // tarkistettavan alueen säde
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;
    private int extraJumpValue = 2;
    public int score;
    public TextMeshProUGUI txtScore;

    // Start is called before the first frame update
    void Start()
    {
        extraJumps = extraJumpValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    {
        // oikea nuoli = 1, vasen nuoli = -1
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (!facingRight && moveInput > 0) {
            // jos ei katsota oikeaan ja painettu oikealle
            Flip();
        }
        else if (facingRight && moveInput < 0) {
            // tai jos katsotaan oikealle ja painettu vasemmalle
            Flip();
        }

         if (rb.transform.position.y < -10)
        {
            SaveScoreAndGoToGameOver();
        }
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    void Update()
    {
        // jos ollaan maassa niin nollataan hypyt:
        if (isGrounded == true) {
            extraJumps = extraJumpValue;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0) {
            rb.velocity = Vector2.up * jumpforce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true) {
            rb.velocity = Vector2.up * jumpforce;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Spike")
        {            
            SaveScoreAndGoToGameOver();
        }
        
        if (col.gameObject.tag  == "Pancake") 
        {
            score++;
            PlayerPrefs.SetInt("PlayerScore", score);
            PlayerPrefs.Save();
            if (txtScore != null)
            {
                txtScore.text = "Pisteet: " + score;
                col.gameObject.SetActive(false);
            }
        }
        if (col.gameObject.tag == "Door")
        {
            // Load next level
            txtScore.text = "Pisteet: " + score;
            SaveScoreAndGoToNextLevel("SceneGame2");
        }
    }	

    void SaveScoreAndGoToGameOver()
    {
        PlayerPrefs.SetInt("PlayerScore", score);
        SceneManager.LoadScene("GameOver");
    }

    void SaveScoreAndGoToNextLevel(string nextLevelName) {
        PlayerPrefs.SetInt("PlayerScore", score);
        SceneManager.LoadScene(nextLevelName);
    }
}
