using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plane_Mechanics : MonoBehaviour
{
    //first script
    public BuoyancyObject BO;

    [SerializeField]private TextMeshProUGUI throttleText;
    [SerializeField]private TextMeshProUGUI speedText;
    
    //primitives
    public float energy = 0f;

    //thrust
    public float throttle = 0.0f;
    public float thrust = 50f;

    public float airSpeed = 0f;
    
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        BO = GetComponent<BuoyancyObject>(); // Get reference to Buoyancy component
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyGravity();

        // Apply throttle and force to aircraft
        ApplyThrottle(throttle);
        
        
        
    }
    
    // Apply throttle to plane
    void ApplyThrottle(float percentage)
    {
        throttleText.text = throttle.ToString();
        speedText.text = rb.velocity.magnitude.ToString();


        float throttleInput = Input.GetAxis("Vertical");
        float yawHor = Input.GetAxis("Horizontal");
        float roll = Input.GetAxis("Roll");

        ApplyYawInput(yawHor);
        ApplyRollInput(roll);

        // Increase or decrease throttle based on input
        float newThrottle = throttle + (throttleInput * Time.deltaTime * 1f);
        throttle = Mathf.Clamp01(newThrottle); // Clamp between 0 and 1
        
        float currentThrust = throttle * thrust;
        float desiredThrust = throttle * thrust;
        if (throttle == 1f)
        {
            desiredThrust = thrust;
        }
        
        float newThrust = Mathf.Lerp(currentThrust, desiredThrust, Time.deltaTime);
        rb.AddRelativeForce(Vector3.forward * newThrust, ForceMode.Force);

        // Set velocity to forward direction of aircraft multiplied by its speed
        

        // Calculate the new airspeed based on the difference between the target and current thrust
        float targetThrust = percentage * thrust;
        float targetSpeed = targetThrust / rb.mass;
        float newSpeed = Mathf.MoveTowards(airSpeed, targetSpeed, Time.deltaTime * 30f);
        airSpeed = newSpeed;

        // // Apply the airspeed as a force in the forward direction of the plane
        // rb.AddRelativeForce(Vector3.forward * airSpeed, ForceMode.Force);
    }
    
    
    // Apply gravity to plane
    void ApplyGravity()
    {   
        float currentThrust = throttle * thrust;
        float weight = rb.mass * Mathf.Abs(Physics.gravity.y);
        float netForce = currentThrust - weight;
        float acceleration = netForce / weight;
        rb.AddForce(transform.up * weight, ForceMode.Force);
        
        rb.velocity = transform.forward * rb.velocity.magnitude;
        

        float rotationSpeed = 5f;
        float dotProduct = Vector3.Dot(transform.forward, Vector3.up);
        if (rb.velocity.magnitude < 0.3f && Vector3.Dot(transform.forward, Vector3.up) > 0.8f ) 
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -Physics.gravity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else if (rb.velocity.magnitude < 0.3f && Vector3.Dot(transform.forward, Vector3.up) > 0.2f ) 
        {
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -Physics.gravity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
    void ApplyYawInput(float input)
    {
        float yawSpeed = 50f;
        float rotationAngle = input * yawSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, transform.up, rotationAngle);
    }

    void ApplyRollInput(float input)
    {
        float rollSpeed = 150f;
        float rotationAngle = input * rollSpeed * Time.deltaTime;
        transform.RotateAround(transform.position, transform.forward, rotationAngle);
    }

}