using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMovementAndCollision : MonoBehaviour
{
    public float sphereRadius;

    void Update()
    {
        // Check if the particle is outside the imaginary sphere
        if (transform.position.magnitude > sphereRadius)
        {
            ReflectParticle();
        }
        else
        {
            transform.position += GetComponent<Rigidbody>().velocity * Time.deltaTime;
        }
    }

    void ReflectParticle()
    {
        // Reflect the particle's position upon reaching the sphere boundary
        transform.position = transform.position.normalized * sphereRadius;

        // Reverse the velocity component
        Vector3 normal = (transform.position - Vector3.zero).normalized;
        GetComponent<Rigidbody>().velocity -= 2 * Vector3.Dot(GetComponent<Rigidbody>().velocity, normal) * normal;
    }
}
