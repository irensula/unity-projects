using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed = 1f;
    public Transform player;
    // Start is called before the first frame update
    public void Init(Transform playerTransform)
    {
        player = playerTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 targetPos = player.position;
        Vector3 dir = (targetPos - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        
        transform.position += Vector3.up * Mathf.Sin(Time.time * 2f) * 0.001f;

        if (dir != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 5f);

        if (Vector3.Distance(transform.position, targetPos) < 0.5f)
            Destroy(gameObject);
    }
}
