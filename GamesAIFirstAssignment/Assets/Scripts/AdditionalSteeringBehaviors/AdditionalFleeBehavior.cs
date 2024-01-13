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
    private Vector3 velocitySeek;
    private Vector3 velocityFlee;
    private float distanceFromCenter = 3;
    private float distanceSpeedRatioFlee;
    private float distanceSpeedRatioSeek;
    private float euclideanDistance;
    private Vector3 desiredVelocity;

    void Update()
    {
        UpdateSteering();
        UpdatePosition();
    }

    private void UpdateSteering()
    {
        desiredVelocity = targetParticle.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocityFlee = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        velocitySeek = Vector3.ClampMagnitude(velocity - steering, MaxVelocity);
    }

    private void UpdatePosition()
    {
        euclideanDistance = Vector3.Distance(transform.position, targetParticle.transform.position);
        distanceSpeedRatioFlee = Mathf.Pow(euclideanDistance / distanceFromCenter, 3);
        distanceSpeedRatioSeek = Mathf.Pow(euclideanDistance / distanceFromCenter, 1);

        if (euclideanDistance < distanceFromCenter)
        {
            transform.position += velocityFlee * Time.deltaTime * -1 / distanceSpeedRatioFlee;
        }
        else if (euclideanDistance > distanceFromCenter)
        {
            transform.position -= velocitySeek * Time.deltaTime * distanceSpeedRatioSeek;
        }

    }
}