using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeBehavior : MonoBehaviour
{
    public GameObject targetParticle;
    public float Mass = 1;
    public float MaxVelocity = 3;
    public float MaxForce = 15;
    private Vector3 velocity;
    private float distanceFromCenter;
    private float distanceSpeedRatio;
    private float euclideanDistance;
    private SpriteRenderer spriteRenderer;
    private Vector3 desiredVelocity;

    void Start()
    {
        InitializeVariables();
    }

    void Update()
    {
        UpdateSteering();
        UpdateColor();
        UpdatePosition();
        DrawDebugRays();
    }

    private void InitializeVariables()
    {
        TargetParticleMovement targetParticleComponent = targetParticle.GetComponent<TargetParticleMovement>();
        transform.position = new Vector2(targetParticleComponent.center.x, targetParticleComponent.center.y);
        velocity = Vector3.zero;
        distanceFromCenter = targetParticleComponent.distanceFromCenter;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateSteering()
    {
        desiredVelocity = targetParticle.transform.position - transform.position;
        desiredVelocity = desiredVelocity.normalized * MaxVelocity;

        var steering = desiredVelocity - velocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering /= Mass;

        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);

        if (velocity != Vector3.zero) // Avoids making the object look at the target when it's not moving
        {
            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg + 90;
            transform.rotation =  Quaternion.Euler(0, 0, angle);
        }
    }

    private void UpdateColor()
    {
        euclideanDistance = Vector3.Distance(transform.position, targetParticle.transform.position);
        // Become more red as the target get closer to the particle
        Color dynamicColor = new Color(distanceFromCenter / euclideanDistance * 1.6f, euclideanDistance / distanceFromCenter * euclideanDistance / distanceFromCenter, euclideanDistance / distanceFromCenter * euclideanDistance / distanceFromCenter, 1);
        spriteRenderer.color = dynamicColor;
    }

    private void UpdatePosition()
    {
        distanceSpeedRatio = Mathf.Pow(euclideanDistance / distanceFromCenter, 3) + 0.00001f;// To solve first frame division by zero
        transform.position += velocity * Time.deltaTime * -1 / distanceSpeedRatio; 
    }
    private void DrawDebugRays()
    {
        Debug.DrawRay(transform.position, velocity.normalized * 2, Color.green);
        Debug.DrawRay(transform.position, desiredVelocity.normalized * 2, Color.magenta);
    }
}