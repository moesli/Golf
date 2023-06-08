using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFollow2 : MonoBehaviour
{
    public GameObject golfBall;
    public Vector3 offset = new Vector3(20f,40f,20f); // The offset between the camera and the ball

    void Update()
    {
        // Update the camera's position to follow the ball, maintaining the initial offset
        transform.position = new Vector3(golfBall.transform.position.x + offset.x, golfBall.transform.position.y + offset.y, golfBall.transform.position.z + offset.z);
        //transform.rotation = Quaternion.Euler(90f, golfBall.transform.eulerAngles.y, 0f);
    }
}
