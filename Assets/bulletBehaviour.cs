using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsUntilDestroyed;

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

        Debug.Log(secondsUntilDestroyed);

        secondsUntilDestroyed -= Time.deltaTime;

        // a fun way to make the bullets enlarge and pop out of existance!
       if (secondsUntilDestroyed < 1)
        {
            transform.localScale = Vector3.one * secondsUntilDestroyed;
        }

/*
        if (secondsUntilDestroyed < 1)
        {
            transform.localScale *= secondsUntilDestroyed;
        }


        if (secondsUntilDestroyed < 0)
        {
            Destroy(gameObject);
        }*/
        
    }

    private void OnCollisionEnter(Collision thisCollision)
    {
        // when we collide, we can ask the thing we collided with for a bunch of information (e.g. Type, Rigidbody)
        // we're interested in the gameobject so we get more info on that specific object that we hit
        // from this we can look for a specific Component (the ones added in Unity like Rigidbody, transform, material, mesh renderer etc)
        // and we can specify the game component we're interested in which is the Class enemyBehaviour
        // this is all so we can determine whether the thing we hit, is an enemy

        /*
         // this was the first version, but Tom then created a new variable for thisCollision.gameObject to make it easier to read
        if (thisCollision.gameObject.GetComponent<enemyBehaviour>() != null)
        {
            Destroy(thisCollision.gameObject);  // destroy what we collided with
            Destroy(gameObject);  // destroy ourselves
        }
        */

        GameObject theirGameObject = thisCollision.gameObject;

        if (theirGameObject.GetComponent<enemyBehaviour>() != null)
        {
            Destroy(theirGameObject);  // destroy what we collided with
            Destroy(gameObject);  // destroy ourselves
        }


    }
}
