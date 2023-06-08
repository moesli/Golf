using UnityEngine;

public class BallFollow : MonoBehaviour
{
    private int hole;
    private float rotationSpeed = 100f;
    public Transform golfBall; // The ball to follow
    private Transform target;
    public Transform target1; // The target to look at
    public Transform target2; // The target to look at
    public Transform target3; // The target to look at
    public BallControll ballControll;
    public float minDistanceBehind = 0.3f; // The minimum distance behind the ball
    public float maxDistanceBehind = 0.8f; // The maximum distance behind the ball
    public float minDistanceAbove = 0.3f; // The minimum distance above the ball
    public float maxDistanceAbove = 0.5f; // The maximum distance above the ball
    public float distanceThreshold = 5f; // The distance at which the camera starts moving farther from the ball
    private bool shotTaken = true; // Flag to track if the shot has been taken


    void Update()
    {
        hole = ballControll.GetHole();
        if(hole == 1){
            target = target1;
        }else if(hole == 2){
            target = target2;
        }else if(hole == 3){
            target = target3;
        }else{
            ballControll.SetHole(1);
            target = target1;
        }
        if(!ballControll.GetFly())
        {
            if (Input.GetKey(KeyCode.Q))
            {
                // Kamera nach links drehen
                transform.RotateAround(golfBall.transform.position, Vector3.up, -rotationSpeed * Time.deltaTime);
                shotTaken = false;
            }
            if (Input.GetKey(KeyCode.E))
            {
                // Kamera nach rechts drehen
                transform.RotateAround(golfBall.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
                shotTaken = false;
            }
        }
        // Check if the shot has been taken
        if (shotTaken)
        {
            // Calculate the direction from the target to the ball
            Vector3 directionToBall = (golfBall.position - target.position).normalized;
            // Calculate the distance from the ball to the target
            float distanceToTarget = Vector3.Distance(golfBall.position, target.position);
            // Lerp between the min and max distance behind based on how close the ball is to the target
            float distanceBehind = Mathf.Lerp(minDistanceBehind, maxDistanceBehind, distanceToTarget / distanceThreshold);
            // Lerp between the min and max distance above based on how close the ball is to the target
            float distanceAbove = Mathf.Lerp(minDistanceAbove, maxDistanceAbove, distanceToTarget / distanceThreshold);
            // Calculate the position behind and above the ball
            Vector3 positionBehindBall = golfBall.position + (directionToBall * distanceBehind) + (Vector3.up * distanceAbove);
            // Set the camera's position
            transform.position = positionBehindBall;
            // Make the camera look at the target
            transform.LookAt(target);
        }
    }

    public void TakeShot()
    {
        shotTaken = true; // Set the flag to true after the shot is taken
    }
}
