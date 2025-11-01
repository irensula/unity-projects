using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // muutetaan Rotation-attribuutteja
        transform.Rotate(new Vector3(100, 250, 350) * Time.deltaTime);   
    }
}
