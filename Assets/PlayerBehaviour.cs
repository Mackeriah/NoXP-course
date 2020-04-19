using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Never set the value of a public variable in code - a different value in the inspector will override the code without telling you
    // If you need to, set it in Start() instead
    public float speed;
    public GameObject bulletPrefeb;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(speed);        
    }

    // Update is called once per frame
    void Update()
    {

        // Find the new position we'll move to
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  // direction player trying to move (x,y,z but we don't care about y so zero)
        
        // Ensure player physics will work
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        
        // Set our velocity
        // (Velocity in a Physics sense is direction * speed)
        ourRigidBody.velocity = inputVector * speed;
        
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
        
        // Face the position the player is moving controller
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);
        
        // If clicked, create a bullet at our current position
        if (Input.GetButton("Fire1"))
        {
            // Instantiate is a function and the stuff after are arguments
            Instantiate(bulletPrefeb, transform.position + transform.forward, transform.rotation);
        }       
 

    }
}
