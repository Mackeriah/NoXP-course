using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Find a rigidBody on this game object (the bullet) and give it back to us
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        ourRigidBody.velocity = transform.forward * bulletSpeed;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
