using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehaviour : MonoBehaviour
{
    public float speed;
    public float visionRange;
    public float visionConeAngle;
    public bool alerted;
    Rigidbody ourRigidBody;
    public Light myLight;

    // Start is called before the first frame update
    void Start()
    {
        alerted = false;
        ourRigidBody = GetComponent<Rigidbody>();  // this is split across the top section and here, so that we can streamline our code, as we use it in 'alerted'
    }

    // Update is called once per frame
    void Update()
    {
        
        if (References.thePlayer != null)  // does player exist?
        {
            Vector3 playerPosition = References.thePlayer.transform.position; // store player position
            Vector3 vectorToPlayer = References.thePlayer.transform.position - transform.position;  // angle from us to player
            myLight.color = Color.white;

            if (alerted)
            {
                // Follow the player
                ourRigidBody.velocity = vectorToPlayer.normalized * speed;
                transform.LookAt(playerPosition);
            }
            else
            {
                // check if we can see the player
                if (Vector3.Distance(transform.position, playerPosition) <= visionRange)  // is the player in range?
                {
                    if (Vector3.Angle(transform.forward, vectorToPlayer) <= visionConeAngle)  // and in our cone?
                    {
                        //alerted = true;
                        myLight.color = Color.red;

                    }
                }

            }
        }
        
    }

    private void OnCollisionEnter(Collision thisCollision)  // When the enemy collides with another GameObject
    {
        GameObject theirGameObject = thisCollision.gameObject;  // store that GameObject in the variable theirGameObject (so we can access it's Components)

        if (theirGameObject.GetComponent<PlayerBehaviour>() != null)  // if the thing we hit has a PlayerBehaviour component
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();  // store its HealthSystem in a variable theirHealthSystem so we can access it

            if (theirHealthSystem != null)  // if the GameObject actually had a HealthSystem component
            {
                theirHealthSystem.TakeDamage(1);  // call the function 'TakeDamage' which is inside the GameObject's HealthSystem and pass the function our damage variable (e.g. 1)
            }            
        }
    }
}
