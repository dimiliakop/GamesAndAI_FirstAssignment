using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEuler : MonoBehaviour
{
    public float gravity = 9.81f; // Gravity value
    public float resistance = 0.1f; // Resistance force value
    public float damping = 0.5f; // Damping coefficient
    private Vector3 velocity; // Particle's velocity

    void Start()
    {
        // Set initial velocity with a random direction and upward movement
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        velocity = (randomDirection + Vector3.up).normalized * Random.Range(8f, 10f);
    }

    void Update()
    {
        // Apply gravity to the particle's velocity
        velocity.y -= gravity * Time.deltaTime;

        // Apply resistance force
        velocity -= resistance * velocity * Time.deltaTime;

        // Update particle's position
        transform.position += velocity * Time.deltaTime;

        // Check for collision with the cube's boundaries and adjust position
        CheckCollisionWithCube();

        if(velocity.y == 0f && velocity.x == 0f && velocity.z == 0)
        {
            Destroy(gameObject);
        }
    }

    void CheckCollisionWithCube()
    {
        // Get the cube's boundaries (you might have predefined these)
        float cubeSize = 3f; // Adjust this value according to your cube's size
        float halfCubeSize = cubeSize * 0.5f;

        Vector3 position = transform.position;

        // Check collision with all faces of the cube
        if (position.x < -halfCubeSize || position.x > halfCubeSize)
        {
            // Particle collided with left or right face of the cube, reverse x velocity and adjust position
            velocity.x *= -damping;
            position.x = Mathf.Clamp(position.x, -halfCubeSize, halfCubeSize);
        }
        if (position.y < -halfCubeSize || position.y > halfCubeSize)
        {
            // Particle collided with bottom or top face of the cube, reverse y velocity and adjust position
            velocity.y *= -damping;
            position.y = Mathf.Clamp(position.y, -halfCubeSize, halfCubeSize);
        }
        if (position.z < -halfCubeSize || position.z > halfCubeSize)
        {
            // Particle collided with front or back face of the cube, reverse z velocity and adjust position
            velocity.z *= -damping;
            position.z = Mathf.Clamp(position.z, -halfCubeSize, halfCubeSize);
        }

        // Update the particle's position after collision adjustment
        transform.position = position;
    }
}
