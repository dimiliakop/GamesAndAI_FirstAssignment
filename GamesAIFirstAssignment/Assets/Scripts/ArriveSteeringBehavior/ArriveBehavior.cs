using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ArriveBehavior : MonoBehaviour
{
    public Transform targetParticle; 
    public float maxSpeed = 5f; // Maximum speed of the Mobile Particle
    public float slowingDistance = 10f; // Distance when reached, the mobile particle begins slowing down
    public float stoppingDistance = 0.3f; // Distance when reached, the mobile stops moving

    private Rigidbody2D arrivingParticle;

    // Start is called before the first frame update
    void Start()
    {
        arrivingParticle = GetComponent<Rigidbody2D>();
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
                if (distance < slowingDistance)
                {
                    desiredSpeed = maxSpeed * (distance / slowingDistance);
                }

                Vector2 desiredVelocity = direction.normalized * desiredSpeed;

                // Calculate the steering force
                Vector2 steering = desiredVelocity - arrivingParticle.velocity;
                arrivingParticle.AddForce(steering);
            }
            else
            {
                arrivingParticle.velocity = Vector2.zero;
                UnityEditor.EditorApplication.isPlaying = false;
                Application.Quit();

            }
        }
    }
}

