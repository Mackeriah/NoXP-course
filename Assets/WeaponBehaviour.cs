using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float secondsBetweenShots;
    private float secondsSinceLastShot;


    // Start is called before the first frame update
    void Start()
    {
        // reset firing timing
        secondsSinceLastShot = secondsBetweenShots;  
    }

    // Update is called once per frame
    void Update()
    {
        // Track how long since last shot
        secondsSinceLastShot += Time.deltaTime;
    }

    public void Fire()  // this is our weapon class
    {
        // Check enough time has passed
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            // Create bullet
            Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
            // Reset timer
            secondsSinceLastShot = 0;
        }
    }

}
