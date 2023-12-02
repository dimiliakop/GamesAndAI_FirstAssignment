using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleCheckCollision : MonoBehaviour
{
    public GameObject particlePrefab;
    public float gravityStrength = 5f;
    public float dragCoefficient = 0.5f;
    public float cubeSize = 2f;
    public float bounceFactor = 0.8f;

    private GameObject particleObject;
    private Rigidbody particleRigidbody;

    private void Start()
    {
        particleObject = Instantiate(particlePrefab, Random.insideUnitSphere * cubeSize, Quaternion.identity);
        particleRigidbody = particleObject.GetComponent<Rigidbody>();
        Vector3 randomVelocity = Random.insideUnitSphere * 5f;
        particleRigidbody.velocity = randomVelocity;
    }

    private void FixedUpdate()
    {
        // Apply gravity
        particleRigidbody.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);

        // Check collision with cube walls and apply bounce if necessary
        Vector3 particlePosition = particleObject.transform.position;

        if (Mathf.Abs(particlePosition.x) > cubeSize)
        {
            particleRigidbody.velocity = new Vector3(-particleRigidbody.velocity.x * bounceFactor, particleRigidbody.velocity.y, particleRigidbody.velocity.z);
        }

        if (Mathf.Abs(particlePosition.y) > cubeSize)
        {
            particleRigidbody.velocity = new Vector3(particleRigidbody.velocity.x, -particleRigidbody.velocity.y * bounceFactor, particleRigidbody.velocity.z);
        }

        if (Mathf.Abs(particlePosition.z) > cubeSize)
        {
            particleRigidbody.velocity = new Vector3(particleRigidbody.velocity.x, particleRigidbody.velocity.y, -particleRigidbody.velocity.z * bounceFactor);
        }

        // Apply drag force
        Vector3 velocityDirection = particleRigidbody.velocity.normalized;
        Vector3 dragForce = -velocityDirection * dragCoefficient;
        particleRigidbody.AddForce(dragForce, ForceMode.Acceleration);
    }
}

