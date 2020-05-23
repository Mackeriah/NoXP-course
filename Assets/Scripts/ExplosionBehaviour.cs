using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{

    public float totalSecondsToExist;
    private float secondsWeveExistedSoFar;

    // Start is called before the first frame update
    void Start()
    {
        secondsWeveExistedSoFar = 0;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        secondsWeveExistedSoFar += Time.deltaTime;

        float lifeFraction = secondsWeveExistedSoFar / totalSecondsToExist;
        Vector3 maxScale = Vector3.one * 5;
        transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, lifeFraction);

        if (secondsWeveExistedSoFar >= totalSecondsToExist)
        {
            Destroy(gameObject);
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        // on collision, store the objects health system component (if it has one)
        HealthSystem theirHealthSystem = other.gameObject.GetComponent<HealthSystem>();

        // this is checking that it had one
        if (theirHealthSystem != null)
        {
            theirHealthSystem.TakeDamage(10);
        }
    }

}
