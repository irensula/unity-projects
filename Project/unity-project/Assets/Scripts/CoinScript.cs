using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    private float speed = 0.8f;
    void Update()
    {
        // change Rotation-attributes
        transform.Rotate(new Vector3(100, 250, 350) * Time.deltaTime * speed);   
    }
}
