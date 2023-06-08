using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandControll : MonoBehaviour
{
    public BallControll ballControll;
    private Vector3 newLocation;
    private Rigidbody rb;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolfBall")) 
        {
            rb = other.GetComponent<Rigidbody>();
            rb.drag = 0.6f;
            rb.angularDrag = 6f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GolfBall")) 
        {
            rb = other.GetComponent<Rigidbody>();
            rb.drag = 0.1f;
            rb.angularDrag= 0.6f;
        }
    }
}
