using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    public float bulletSpeed;
    public float secondsUntilDestroyed;
    public float damage;    

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
        /*if (secondsUntilDestroyed < 1)
        {
            transform.localScale = Vector3.one * secondsUntilDestroyed;
        }*/


        if (secondsUntilDestroyed < 1)
        {
            transform.localScale *= secondsUntilDestroyed;
        }


        if (secondsUntilDestroyed < 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision thisCollision)  // When the bullet collides with another GameObject
    {
        GameObject theirGameObject = thisCollision.gameObject;  // store that GameObject in the variable theirGameObject (so we can access it's Components)

        if (theirGameObject.GetComponent<enemyBehaviour>() != null)  // if the thing we hit has an enemyBehaviour component
        {
            HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();  // store its HealthSystem in a variable theirHealthSystem so we can access it

            if (theirHealthSystem != null )  // if the GameObject has a HealthSystem component
            {
                theirHealthSystem.TakeDamage(damage);  // call the function 'TakeDamage' which is inside the GameObject's HealthSystem and pass the function our damage variable (e.g. 1)
            }                        

            Destroy(gameObject);  // now regardless of anything else, we (the bullet) destroy ourselves
        }
    }

        // the below are the old code and comments, which I'm keeping for now.  The new ones above are nicely concise, but only possible as I know understand more about this!

        /*
        private void OnCollisionEnter(Collision thisCollision)
        {
            // when we collide, we can ask the thing we collided with for a bunch of information (e.g. Type, Rigidbody)
            // we're interested in the gameobject so we get more info on that specific object that we hit
            // from this we can look for a specific Component (the ones added in Unity like Rigidbody, transform, material, mesh renderer etc)
            // and we can specify the game component we're interested in which is the Class enemyBehaviour
            // this is all so we can determine whether the thing we hit, is an enemy       

            GameObject theirGameObject = thisCollision.gameObject;

            if (theirGameObject.GetComponent<enemyBehaviour>() != null)  // if the thing we hit has an enemyBehaviour component
            {
                // we're now creating a new variable to access the health of object that we've collided with
                // datatype = HealthSystem (created when we created the HealthSystem component/Script)
                // we've already created the variable theirGameObject from the collision (a few lines up)
                // we now access that objects health via it's HealthSystem component

                HealthSystem theirHealthSystem = theirGameObject.GetComponent<HealthSystem>();

                theirHealthSystem.TakeDamage(damage);
                // now we use the function within the HealthSystem component to handle damage (damage is declared above and set in Inspector in the bullet template)

                Destroy(gameObject);  // now regardless of anything else, we (the bullet) destroy ourselves
            }

        }
        */
}
