using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveBehavior : MonoBehaviour
{
    public Transform targetParticle;
    public float maxSpeed = 5f; 
    public float slowingRadius = 13f; 
    public float stoppingDistance = 0.3f; 
    public float acceleration = 1.0f; 

    private Rigidbody2D arrivingParticle;
    private Vector2 currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        arrivingParticle = GetComponent<Rigidbody2D>();
        currentVelocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        ArrivalCheck();
    }

    private void ArrivalCheck()
    {
        if (targetParticle != null)
        {
            Vector2 direction = targetParticle.position - transform.position;
            float distance = direction.magnitude;

            if (distance > stoppingDistance)
            {
                float desiredSpeed = maxSpeed;
                if (distance < slowingRadius)
                {
                    desiredSpeed = maxSpeed * (distance / slowingRadius);
                }

                // Calculate desired velocity based on acceleration
                Vector2 desiredVelocity = direction.normalized * desiredSpeed;
                Vector2 accelerationVector = (desiredVelocity - currentVelocity) * acceleration;

                // Update velocity using Euler integration
                currentVelocity += accelerationVector * Time.deltaTime;
                arrivingParticle.velocity = currentVelocity;

                // Calculate the steering force
                Vector2 steering = currentVelocity - arrivingParticle.velocity;
                arrivingParticle.AddForce(steering);
            }
            else
            {
                arrivingParticle.velocity = Vector2.zero;
                //UnityEditor.EditorApplication.isPlaying = false;
                //Application.Quit();
            }
        }
    }
}