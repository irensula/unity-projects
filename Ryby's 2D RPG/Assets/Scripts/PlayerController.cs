using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    Rigidbody2D rigidbody2d;
    Vector2 move;
    public int maxHealth = 5;
    // int currentHealth;
    // curentHEalth = maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        MoveAction.Enable();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>(); // get the current value of the Move Action
        Debug.Log(move);
    }
    void FixedUpdate() {
        Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    // void ChangeHealth (int amoubt)
    // {
    //     currentHealth = Math.Clamp(currentHealth + amount, 0, maxHealth);
    //     Debug.Log(currentHealth + "/" + maxHealth);
    // }
}
