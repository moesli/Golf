using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed = 5f; // The speed at which the camera rotates
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player's input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the new rotation
        float horizontalRotation = horizontalInput * rotateSpeed * Time.deltaTime;
        float verticalRotation = verticalInput * rotateSpeed * Time.deltaTime;

        // Apply the rotation to the camera
        transform.Rotate(new Vector3(-verticalRotation, horizontalRotation, 0));
    }
}
