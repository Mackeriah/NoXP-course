using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float accuracy;
    public float secondsBetweenShots;
    public float numberOfProjectiles;
    private float secondsSinceLastShot;


    void Start()
    {
        // reset firing timing
        secondsSinceLastShot = secondsBetweenShots;  
    }


    void Update()
    {
        // Track how long since last shot
        secondsSinceLastShot += Time.deltaTime;
    }


    public void Fire(Vector3 targetPosition)  // this is our weapon class, target position accepts a v3 which currently is the mouse cursor position (cursorPosition)
    {
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            // Create bullet(s) and store in variable
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);

                // Offset target position by random amount according to our inaccuracy which increases the further away the target is
                float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
                Vector3 inaccuratePosition = targetPosition;
                inaccuratePosition.x += Random.Range(-inaccuracy, inaccuracy);
                inaccuratePosition.z += Random.Range(-inaccuracy, inaccuracy);

                // Look at target position 
                newBullet.transform.LookAt(inaccuratePosition);

                // Reset timer
                secondsSinceLastShot = 0;
            }                        
        }
    }

}
