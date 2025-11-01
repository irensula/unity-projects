using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    public int playerScore = 0;
    public float playerTime = 0f;
    public TextMeshProUGUI txtScore;
    public TextMeshProUGUI txtTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(move);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "pickup") 
        {
            col.gameObject.SetActive(false);
            playerScore++;
            txtScore.text = "Score: " + playerScore;
            Debug.Log("Collide" + playerScore);

            if (playerScore >= 6)
            {
                PlayerPrefs.SetInt("PlayerScore", playerScore);
                PlayerPrefs.SetFloat("PlayerTime", playerTime);
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        playerTime += 1 * Time.deltaTime;
        txtTime.text = "Time: " + Mathf.Round(playerTime);
    }
}
