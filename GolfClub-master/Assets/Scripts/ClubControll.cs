using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ClubControll : MonoBehaviour
{
    public enum ClubType
    {
    Driver,   // For long distances, low loft
    Iron,     // For shorter distances, higher loft
    Putter    // For putting, no loft
    }

    public ClubType clubType; // Set this in the Unity inspector
    public GameObject golfBall;
    public BallControll ballControll;
    public BallFollow ballFollow;
    public ScoreManager scoreManager;
    public Transform parent;
    public float chargeTime;

    private float rotationSpeed = 100f;
    private float chargeRate = 10f;
    private float swingPower;
    private float swingStep;
    private float minSwingPower;
    private float maxSwingPower;
    public Vector3 initialPosition;
    public Quaternion initialRotation;
    private Rigidbody rb;
    private Vector3 swingDirection;
    public bool isCharging;
    private bool isChargingClub;
    private bool isSwinging;
    private bool isSwingingClub;
    private bool start;

    public AudioClip hitSound; // The sound to play when the club hits the ball
    private AudioSource audioSource;

    float threshold = 1f;
    

    void Start()
    {
        rb = golfBall.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        RotateClub();
        ChargeSwing();

        if (isChargingClub)
        {
            ClubSwing(90f, -150f);
        }
        if (isSwingingClub)
        {
            ClubSwing(680f, 90f);
        }
    }

    void RotateClub()
    {
        if(!isCharging && !isSwinging)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            transform.RotateAround(golfBall.transform.position, Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
            swingDirection = (golfBall.transform.position - transform.position).normalized;
            initialPosition = transform.localPosition;
            initialRotation = transform.localRotation;
            switch (clubType)
            {
                case ClubType.Driver:
                    swingDirection.y = 0.1f; // Low loft
                    swingPower = 15f;
                    minSwingPower = 40f;
                    maxSwingPower = 80f;
                    break;
                case ClubType.Iron:
                    swingDirection.y = 0.2f; // Higher loft
                    swingPower = 7.5f;
                    minSwingPower = 20f;
                    maxSwingPower = 40f;
                    break;
                case ClubType.Putter:
                    swingDirection.y = 0f; // No loft
                    swingPower = 5f;
                    minSwingPower = 1f;
                    maxSwingPower = 20f;
                    break;
            }
        }
    }

    void ChargeSwing()
    {
        if (rb.velocity.magnitude < 0.3f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isCharging)
            {
                // Start charging when the Space key is first pressed down
                isCharging = true;
                start = true;
                isChargingClub = true;
                chargeTime = 0;
            }
            else if (Input.GetKey(KeyCode.Space) && isCharging)
            {
                chargeTime += Time.deltaTime * chargeRate;
            }
            else if (Input.GetKeyUp(KeyCode.Space) && isCharging)
            {
                // Stop charging when the Space key is released
                isCharging = false;
                isChargingClub = false;
                isSwinging = true;
                isSwingingClub = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfBall") && isSwinging)
        {
            isSwinging = false;
            isSwingingClub = false;
            ballControll.currentLocation = golfBall.transform.position;
            float finalSwingPower = Mathf.Clamp(chargeTime * swingPower, minSwingPower, maxSwingPower);
            Swing(finalSwingPower);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall") && isSwinging)
        {
            isSwinging = false;
            isSwingingClub = false;
            ballControll.currentLocation = golfBall.transform.position;
            float finalSwingPower = Mathf.Clamp(chargeTime * swingPower, minSwingPower, maxSwingPower);
            audioSource.PlayOneShot(hitSound);
            Swing(finalSwingPower);
        }
    }

    void Swing(float power)
    {
        //rb.isKinematic = false;
        ballFollow.TakeShot();
        rb.AddForce(swingDirection * power, ForceMode.Impulse);
        scoreManager.IncrementScore();
        ResetClub();
    }

    void ClubSwing(float swingSpeed, float swingAngle)
    {
        if(swingAngle < 0f)
        {
            // Calculate the amount to rotate this frame
            swingStep = -swingSpeed * Time.deltaTime;
        }else
        {
            // Calculate the amount to rotate this frame
            swingStep = swingSpeed * Time.deltaTime;
        }

        // Rotate the club
        transform.Rotate(swingStep, 0f, 0f, Space.Self);

        // Check if the club has rotated enough
        float currentAngle = transform.localRotation.eulerAngles.x;
        float targetAngle = Quaternion.Euler(swingAngle, 0f, 0f).eulerAngles.x;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

        // Check if the difference is within the threshold || Mathf.Abs(currentAngle - targetAngle) > 200f - threshold
        if(isChargingClub)
        {
            if (Mathf.Abs(currentAngle - targetAngle) < threshold)
            { 
                isChargingClub = false;
            }
        }
        // Check if the difference is within the threshold
        if(isSwingingClub)
        {
            if (Mathf.Abs(currentAngle - targetAngle) < threshold)
            { 
                isSwingingClub = false;
                ResetClub();
            }
        }
    }

    void ResetClub(){
        transform.localPosition = initialPosition;
        initialRotation.x = 0f;
        initialRotation.y = 0f;
        transform.localRotation = initialRotation;
    }

}
