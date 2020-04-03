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
        
        // Movement     
        float maxDistanceToMove = speed * Time.deltaTime;

        // Find the new position we'll move to
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  // direction player trying to move (x,y,z but we don't care about y so zero)
        Vector3 movementVector = inputVector * maxDistanceToMove;  // max disatnce player can move
        Vector3 newPosition = transform.position + movementVector; // where we will be moving TO

        transform.LookAt(newPosition);  // Face the new position
        transform.position = newPosition;  // Actually move to the new position

        // old movement code
        // This is the equivilant of what you use in GM, as GetAxis is either 1 or 0
        // transform.position += Vector3.forward * Input.GetAxis("Vertical") * maxDistanceToMove;
        // transform.position += Vector3.right * Input.GetAxis("Horizontal") * maxDistanceToMove;
        

        // Firing
        // If clicked, create a bullet at our current position
        if (Input.GetButton("Fire1"))
        {
            // Instantiate is a function and the stuff after are arguments
            Instantiate(bulletPrefeb, transform.position + transform.forward, transform.rotation);
        }       
 

    }
}
