using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float speed;
    public List<WeaponBehaviour> weapons = new List<WeaponBehaviour>();
    public int selectedWeaponIndex;

    // Start is called before the first frame update
    void Start()
    {
        References.thePlayer = gameObject;
        selectedWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Find the new position we'll move to
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));  // direction player trying to move (x,y,z but we don't care about y so zero)
        
        // Ensure player physics will work
        Rigidbody ourRigidBody = GetComponent<Rigidbody>();
        
        // Set our velocity
        // (Velocity in a Physics sense is direction * speed)
        ourRigidBody.velocity = inputVector * speed;
        
        Ray rayFromCameraToCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraToCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraToCursor.GetPoint(distanceFromCamera);
        
        // Face the position the player is moving controller
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);

        
        // Firing
        if (weapons.Count > 0 && Input.GetButton("Fire1")) // Fire1 is LMB
        {
            // Tell our weapon to fire (using the Class "Fire" which is assigned to the weapon) using our cursor position as an argument
            weapons[selectedWeaponIndex].Fire(cursorPosition);
        }

        // Firing
        if (Input.GetButtonDown("Fire2")) // Fire1 is RMB
        {
            selectedWeaponIndex += 1;
            if (selectedWeaponIndex >= weapons.Count)  // if we're trying to select beyond the available weapon slots
            {
                selectedWeaponIndex = 0;  // go to first weapon slot
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        // when we collide with something, ask it if it's PARENT has a WeaponBehaviour and if it does, return the weapon
        WeaponBehaviour theirWeapon = other.GetComponentInParent<WeaponBehaviour>();
        if (theirWeapon != null)
        {
            weapons.Add(theirWeapon);
            theirWeapon.transform.position = transform.position;
            theirWeapon.transform.rotation = transform.rotation;
            theirWeapon.transform.SetParent(transform);
        }

    }


}