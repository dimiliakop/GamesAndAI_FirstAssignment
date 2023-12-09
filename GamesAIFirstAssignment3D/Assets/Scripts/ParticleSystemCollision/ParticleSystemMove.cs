using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticleCheckCollision : MonoBehaviour
{
    public GameObject particlePrefab;
    public float gravityStrength = 5f;
    public float dragCoefficient = 0.5f;
    public float cubeSize = 2f;
    public float bounceFactor = 0.8f;
    public float maxInitialVelocity = 5f;

    private Rigidbody particleRigidbody;

    private void Start()
    {
        //particlePrefab = Instantiate(particlePrefab, Random.insideUnitSphere * cubeSize, Quaternion.identity);
        particleRigidbody = particlePrefab.GetComponent<Rigidbody>();
        Vector3 randomVelocity = Random.insideUnitSphere * 5f;
        particleRigidbody.velocity = randomVelocity;
    }

    private void Update()
    {

    }

    //private void ApplyForce(Rigidbody particle)
    //{
    //    particle.AddForce(Vector3.up * gravityStrength, ForceMode.Acceleration);
    //}

    //private void CheckCubeWallsCollision(Rigidbody particle)
    //{
    //    Vector3 particlePosition = particlePrefab.transform.position;

    //    if (Mathf.Abs(particlePosition.x) > cubeSize)
    //    {
    //        particle.velocity = new Vector3(-particle.velocity.x * bounceFactor, particle.velocity.y, particle.velocity.z);
    //    }

    //    if (Mathf.Abs(particlePosition.y) > cubeSize)
    //    {
    //        particle.velocity = new Vector3(particle.velocity.x, -particle.velocity.y * bounceFactor, particle.velocity.z);
    //    }

    //    if (Mathf.Abs(particlePosition.z) > cubeSize)
    //    {
    //        particle.velocity = new Vector3(particle.velocity.x, particle.velocity.y, -particle.velocity.z * bounceFactor);
    //    }
    //}

    //private void ApplyDragForce(Rigidbody particle)
    //{
    //    Vector3 velocityDirection = particle.velocity.normalized;
    //    Vector3 dragForce = -velocityDirection * dragCoefficient;
    //    particle.AddForce(dragForce, ForceMode.Acceleration);
    //}
}

