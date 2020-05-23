using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthSystem : MonoBehaviour
// The above HealthSystem is a new component (AKA Script) we've created
// this also means we've created a new datatype "HealthSystem" (like float, vector or GameObject)

{
    [FormerlySerializedAs("health")]
    public float maxHealth;  // this is where each gameobject's max health is set
    private float currentHealth;
    public GameObject healthBarPrefab;    // this creates a slot to drag a prefab to be used by the gameObject 
    public GameObject deathEffectPrefab;  // this creates a slot to drag a prefab to be used by the gameObject
    private HealthBarBehaviour myHealthBar;  // store this behaviour in myHealthBar variable    


    void Start()
    {
        // set our health to max on object creation
        currentHealth = maxHealth;

        // create our health panel ON the canvas as References.canvas
        // we are using instantiate a little differently here as we're not specifying the x,y,z coords and rotation
        // instead we are telling the healthBarPrefab simply that it needs to be on the canvas, as just stating transform
        // means it's default x,y,z and rotation are used
        GameObject healthBarObject = Instantiate(healthBarPrefab, References.canvas.transform);
        myHealthBar = healthBarObject.GetComponent<HealthBarBehaviour>();

    }

    public void TakeDamage(float damageAmount)
    // Void means it doesn’t need any information returned AFTER an object has called this function
    // damageAmount is where we'll store the value passed to us by the object calling this function (the bullet I believe)
    // Any GameObject that has the HealthSystem component will have a health variable and will use this code
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            // if my death effect prefab exists (currently only enemies have this prefab dragged to the slot, so the player doesn't explode)
            if (deathEffectPrefab != null)
            {
                Instantiate(deathEffectPrefab, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // if my health bar exists (this I think is purely to ensure we don't cause a crash)
        if (myHealthBar != null)
        {
            // this destroys the health bar on the canvas when a gameobject no longer exists
            Destroy(myHealthBar.gameObject);
        }
        
        
    }

    void Update()
    {
        // make our health bar reflect our health.  the ShowHealthFraction method/function requires a fraction to be passed to it
        myHealthBar.ShowHealthFraction(currentHealth / maxHealth);

        // make our health bar follow us by moving to our current position
        myHealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);

    }
}
