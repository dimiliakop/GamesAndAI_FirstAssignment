using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalFleeBehavior : MonoBehaviour
{
    public GameObject targetParticle;
    public float Mass = 1;
    public float MaxVelocity = 3;
    public float MaxForce = 15;
    private Vector3 velocity;
    private float distanceFromCenter = 5;
    private float distanceSpeedRatio;
    private float euclideanDistance;
    private Vector3 desiredVelocity;

    void Start()
    {
        //InitializeVariables();
    }

    void Update()
    {
        UpdateSteering();
        UpdatePosition();
    }

    //private void InitializeVariables()
    //{
      //  TargetParticleMovement targetParticleComponent = targetParticle.GetComponent<TargetParticleMovement>();
    //    transform.position = new Vector2(targetParticle.center.x, targetParticle.center.y);
    //    velocity = Vector3.zero;
    //    distanceFromCenter = targetParticleComponent.distanceFromCenter;
    //}

    private void UpdateSteering()
    {
        desiredVelocity = targetParticle.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
    }

    private void UpdatePosition()
    {
        euclideanDistance = Vector3.Distance(transform.position, targetParticle.transform.position);
        distanceSpeedRatio = Mathf.Pow(euclideanDistance / distanceFromCenter, 3);
        if(euclideanDistance < distanceFromCenter)
        {
            transform.position += velocity * Time.deltaTime * -1 / distanceSpeedRatio;
        }
        else if(euclideanDistance > distanceFromCenter)
        {
            transform.position -= velocity * Time.deltaTime * -1 / distanceSpeedRatio;
        }
        else
        {
            transform.position = velocity * Time.deltaTime * -1 / distanceSpeedRatio;
        }
    }
}