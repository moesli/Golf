using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnControll : MonoBehaviour
{
    public BallControll ballControll;
    private Vector3 newLocation;

    void OnTriggerEnter(Collider other)
    {
        // Check if the ball has hit the target
        if (other.CompareTag("GolfBall")) 
        {
            newLocation = ballControll.currentLocation;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = newLocation;
            other.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
