using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
// The above HealthSystem is a new component (AKA Script) we've created
// this also means we've created a new datatype "HealthSystem" (like float, vector or GameObject)

{
    public float health;

    public void TakeDamage(float damageAmount)
    // Void means it doesn’t need any information returned AFTER an object has called this function
    // damageAmount is where we'll store the value passed to us by the object calling this function (the bullet I believe)
    // Any GameObject that has the HealthSystem component will have a health variable and will use this code
    {
        health -= damageAmount;
        if (health <= 0)  // if my health <= 0
        {
            Destroy(gameObject);  // destroy myself (the GameObject running this code)
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
