using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class WeaponBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float accuracy;
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

    public void Fire(Vector3 targetPosition)  // this is our weapon class, target position accepts a v3 which currently is the mouse cursor position (cursorPosition)
    {
        // Check enough time has passed
        if (secondsSinceLastShot >= secondsBetweenShots)
        {
            // Create bullet and store in variable
            GameObject newBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);

            // Offset target position by random amount according to our inaccuracy which increases the further away the target is
            float inaccuracy = Vector3.Distance(transform.position, targetPosition) / accuracy;
            targetPosition.x += Random.Range(-inaccuracy, inaccuracy);
            targetPosition.z += Random.Range(-inaccuracy, inaccuracy);

            // Look at target position 
            newBullet.transform.LookAt(targetPosition);

            // Reset timer
            secondsSinceLastShot = 0;
        }
    }

}
