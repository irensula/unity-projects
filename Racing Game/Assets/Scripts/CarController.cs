using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarController : MonoBehaviour
{
    public float speed;
    Vector3 position;
    private float minX;
    private float maxX;
    
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position; // initial position of the car

        // find world limits of the screen (camera view)
        Camera cam = Camera.main; // reference to the camera
        float halfWidth = transform.localScale.x / 2f; // count the half of the car
        float zDistance = Mathf.Abs(cam.transform.position.z - transform.position.z); // calculate the distance along the Z axis between the camera and the object

        // convert coordinates from the Viewport system (0...1) to world coordinates
        Vector3 leftBorder = cam.ViewportToWorldPoint(new Vector3(0, 0, zDistance)); 
        Vector3 rightBorder = cam.ViewportToWorldPoint(new Vector3(1, 0, zDistance));

        // set the minimum and maximum x values ​​in which the object can be located
        minX = leftBorder.x + halfWidth;
        maxX = rightBorder.x - halfWidth;
    }

    void FixedUpdate()
    {
        // car's position changings
        position.x += Input.GetAxis("Horizontal") * speed * Time.deltaTime;  
        
        // clamp position so it stays inside the screen
        position.x = Mathf.Clamp(position.x, minX, maxX);

        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D col) {
    // if (col.gameObject.tag == "enemy")
    if (col.gameObject.CompareTag("enemy")) 
        {
            Debug.Log("törmäys vastustajaan" + col.gameObject.name);
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }
}
