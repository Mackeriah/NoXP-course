using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public List<WeaponBehaviour> weaponList = new List<WeaponBehaviour>();
    public int selectedWeaponIndex;
    public int itemsCollected;


    void Start()
    {
        References.thePlayer = gameObject;
        selectedWeaponIndex = 0;
    }


    void Update()
    {
        // Find the new position we'll move to
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  // direction player trying to move (x,y,z but we don't care about y so zero)
        
        // Ensure player physics will work (future Owen:  hang on, isn't THIS bit simply storing 
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        
        // Set our velocity
        ourRigidBody.velocity = inputVector * speed;
        
        // Get mouse cursor position
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
        
        // Face the position the player is moving
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        
        // Firing
        if (weaponList.Count > 0 && Input.GetButton("Fire1")) // If we have a weapon in our list (Fire1 is LMB)
        {
            // Tell our weapon to fire (using the Class "Fire" which is assigned to the weapon) using our cursor position as an argument
            weaponList[selectedWeaponIndex].Fire(cursorPosition);
        }

        // Firing
        if (Input.GetButtonDown("Fire2")) // Fire1 is RMB
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
        }
    }

    private void ChangeWeaponIndex(int index)  // < this is a function or method
    {
        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weaponList.Count)  // if we're trying to select beyond the available weapon slots
        {
            selectedWeaponIndex = 0;  // go to first weapon slot
        }

        // make active/inactive the currently selected/unselected weapon(s)
        for (int i = 0; i < weaponList.Count; i++)
        {
            if (i == selectedWeaponIndex)
            {
                weaponList[i].gameObject.SetActive(true);
            }
            else
            {
                weaponList[i].gameObject.SetActive(false);
            }
            
        }

    }

    // Weapon pickup
    private void OnTriggerEnter(Collider other)
    {
        // when we collide with something, ask it if it's PARENT has a WeaponBehaviour and if it does, return the weapon
        WeaponBehaviour theirWeapon = other.GetComponentInParent<WeaponBehaviour>();
        if (theirWeapon != null)  // as long as they HAVE a weaponbehaviour, do the following
        {
            weaponList.Add(theirWeapon);  // add the weapon to our weaponList (play game and pickup a weapon to see this happening)

            // make the weapon follow us, point in same direction as us and be our child
            theirWeapon.transform.position = transform.position;
            theirWeapon.transform.rotation = transform.rotation;
            theirWeapon.transform.SetParent(transform);
            ChangeWeaponIndex(weaponList.Count - 1);  // select the weapon
        }

    }

    private void OnCollisionEnter(Collision thisCollision)  // When the player collides with another GameObject
    {
        GameObject theirGameObject = thisCollision.gameObject;  // store that GameObject in the variable theirGameObject 

        if (theirGameObject.GetComponent<Collectible>() != null)  // if the thing we hit has a Collectible component
        {
            itemsCollected += 1;
            Destroy(theirGameObject);            
        }
    }


}