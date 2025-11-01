using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour

{
    [SerializeField]
    private float speed;	
    
    private float height;
    private string input;
    public bool isRight;	

    public void Init(bool isRightPaddle) {
        
        isRight = isRightPaddle;
        
        Vector2 pos = Vector2.zero;

        if (isRightPaddle)
        {
            input = "PaddleRight";
            // maila oikealle
            pos = new Vector2 (GameManager.topRight.x, 0);
            pos -= Vector2.right * transform.localScale.x;
        }
        else
        {
            // maila vasemmalle:
            input = "PaddleLeft";
            pos = new Vector2(GameManager.bottomLeft.x, 0);
            pos += Vector2.right * transform.localScale.x;
        }
        // muuttaa sijainnin uudeksi:
        transform.position = pos;
        transform.name = input;
    }
    void Start()
    {
        height = transform.localScale.y;
        if (speed == 0)
            speed = 5f;
    }
    void Update()
    {
        float move = Input.GetAxis(input) * Time.deltaTime * speed;

         if (transform.position.y < GameManager.bottomLeft.y + height / 2 && move < 0)
        {
            move = 0;
        }
        else if (transform.position.y > GameManager.topRight.y - height / 2 && move > 0)
        {
            move = 0;
        }
        
        transform.Translate (move * Vector2.up);
    }
}
