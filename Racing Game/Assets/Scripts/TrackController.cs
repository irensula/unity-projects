using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public float speed;
    Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        // in the vertical direction the y scale moves in proportion to the speed
        offset = new Vector2(0, Time.time * speed);
        GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
    public void AddSpeed() 
    {
        speed++;
        Debug.Log("Track's speed" + speed);
    }
}
