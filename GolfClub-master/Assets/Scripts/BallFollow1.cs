using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollow1 : MonoBehaviour
{
    public GameObject golfBall;
    public Vector3 offset = new Vector3(-0.06f,0.56f,-0.17f);
    private Rigidbody rb;

    void Start()
    {
        // Calculate the initial offset between the camera and the ball
        rb = golfBall.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        // Update the camera's position to follow the ball, maintaining the initial offset
        if (rb.velocity.magnitude < 0.3f){
            transform.position = golfBall.transform.position + offset;
        }
    }

}
