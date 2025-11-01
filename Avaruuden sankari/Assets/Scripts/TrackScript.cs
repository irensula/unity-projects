using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public float speed;
    public Transform[] tracks;
    public float height = 40f;

    void Update()
    {
        foreach (var t in tracks)
        {
            t.position += Vector3.down * speed * Time.deltaTime;

            if (t.position.y < -height)
            {
                float topY = FindTopY();
                t.position = new Vector3(t.position.x, topY + height - 0.5f, t.position.z);
            }
        }
    }

    float FindTopY() 
    {
        float maxY = float.MinValue;
        foreach (var t in tracks)
            if (t.position.y > maxY) maxY = t.position.y;
        return maxY;
    }
}
