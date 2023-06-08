using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetControll : MonoBehaviour
{
    public BallControll ballControll;
    public ScoreManager scoreManager;
    private Vector3 newLocation;
    private Target target;
    public Transform startHole1;
    public Transform startHole2;
    public Transform startHole3;
    private int hole;
    private int score;

    public enum Target
    {
        Target1,   
        Target2,     
        Target3    
    }
    // Start is called before the first frame
    void Start()
    {
        if (System.Enum.TryParse(gameObject.tag, out target)){
            switch(target){
                case Target.Target1:
                    newLocation = startHole2.position;
                    break;
                case Target.Target2:
                    newLocation = startHole3.position;
                    break;
                case Target.Target3:
                    newLocation = startHole1.position;
                    break;
            }
        }
    }

    void Update()
    {
        score = scoreManager.GetScore();
        if (score > 12){
            hole = ballControll.GetHole();
            switch(hole){
                case 1:
                    ballControll.startLocation = startHole2.position;
                    hole += 1; 
                    ballControll.SetHole(hole);
                    scoreManager.SaveScore();
                    ballControll.Teleport(startHole2.position);
                    break;
                case 2:
                    ballControll.startLocation = startHole3.position;
                    hole += 1; 
                    ballControll.SetHole(hole);
                    scoreManager.SaveScore();
                    ballControll.Teleport(startHole3.position);
                    break;
                case 3:
                    ballControll.startLocation = startHole1.position;
                    hole += 1; 
                    ballControll.SetHole(hole);
                    scoreManager.SaveScore();
                    ballControll.Teleport(startHole1.position);
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the ball has hit the target
        if (other.CompareTag("GolfBall")) 
        {
            ballControll.Teleport(newLocation);
            hole = ballControll.GetHole();
            ballControll.startLocation = newLocation;
            hole += 1; 
            ballControll.SetHole(hole);
            scoreManager.SaveScore();
        }
    }
}
