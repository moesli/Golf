using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControll : MonoBehaviour
{
    public float minSpeed = 0.3f; // Die Mindestgeschwindigkeit, die der Ball haben muss, um sich weiterzubewegen
    public Vector3 currentLocation; // Die aktuelle Position des Balls
    public Vector3 startLocation; // Die Startposition des Balls
    private Rigidbody rb; // Der Rigidbody des Balls
    public int hole = 1;
    private bool fly = false;

    void Start()
    {
        // Erhalten Sie die Rigidbody-Komponente
        rb = GetComponent<Rigidbody>();
        currentLocation = startLocation;
        Teleport(startLocation);
    }

    void Update()
    {
        // Überprüfen Sie, ob die Geschwindigkeit des Balls unter der Mindestgeschwindigkeit liegt
        if (rb.velocity.magnitude < minSpeed)
        {
            // Setzen Sie die Geschwindigkeit des Balls auf Null, um ihn zu stoppen
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            fly = false;
        }else{
            fly = true;
        }

        if (Input.GetKeyDown(KeyCode.R)){
            Teleport(currentLocation);
        }
        if (Input.GetKeyDown(KeyCode.Z)){
            Teleport(startLocation);
        }
    }

    public void Teleport(Vector3 data)
    {
        // Setzen Sie die Position des Balls auf die neue Position
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = data;
        fly = false;
    }

    public int GetHole(){
        return hole;
    }

    public void SetHole(int i){
        hole = i;
    }

    public bool GetFly(){
        return fly;
    }
}
