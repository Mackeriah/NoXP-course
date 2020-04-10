using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    // Track the player's position
    public GameObject player;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure enemy physics will work
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();

        // Calculate direction and distance to travel to the player        
        Vector3 vectorToPlayer = player.transform.position - transform.position;
        // Use this as our velocity but normalize the value to 1 metre and then multiply by our speed
        // (Velocity in a Physics sense is direction * speed)
        ourRigidBody.velocity = vectorToPlayer.normalized * speed;

    }
}
