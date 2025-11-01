using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float speed = 5f; // asteroid's speed
    private Vector2 target; // coordinates where asteroid flies
    private float rotationSpeed = 10f;
    
    public Sprite[] asteroidSprites;
    private SpriteRenderer spriteRenderer;
     // initialize the asteroid's target
    public void Init(Vector2 targetPosition) 
    {
        target = targetPosition; // target position where the asteroid flies
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (asteroidSprites.Length > 0)
        {
            int index = Random.Range(0, asteroidSprites.Length);
            spriteRenderer.sprite = asteroidSprites[index];
        }
        transform.Rotate(0,0, Random.Range(0f, 360f));
    }
    // Update is called once per frame
    void Update()
    {
        // Move toward the target
        Vector2 currentPosition = transform.position;
        Vector2 direction = (target - currentPosition).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
 
         // Rotate around Z axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Destroy asteroid if it reaches the target
        if (Vector2.Distance(currentPosition, target) < 0.1f) {
            Destroy(gameObject);
        }
    }
}
