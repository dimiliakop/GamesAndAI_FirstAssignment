using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParticleMovement : MonoBehaviour
{
    public int distanceFromCenter = 5;
    public Vector2 center;
    public float speed = 1.0f;
    public float changeSpeedInterval = 2.0f;
    private SpriteRenderer spriteRenderer;
    private float totalAngle;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        UpdateColor();

    }

    private void UpdatePosition()
    {

        totalAngle += Time.deltaTime * speed; // Increment totalAngle based on speed

        float x = Mathf.Cos(totalAngle) * distanceFromCenter;
        float y = Mathf.Sin(totalAngle) * distanceFromCenter;
        transform.position = new Vector2(x + center.x, y + center.y);
    }
    private void InitializeVariables()
    {
        InvokeRepeating("UpdateSpeed", 5.0f, changeSpeedInterval); //The function will call every 5 seconds after the first 5 seconds
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void UpdateSpeed()
    {
        speed = UnityEngine.Random.Range(0.5f, 2.0f);
    }
    private void UpdateColor()
    {
        // Become more green as the particle get closer to the target
        Color dynamicColor = new Color(speed / 2.0f, speed / 2.0f, 1 - speed / 2.0f, 1);
        spriteRenderer.color = dynamicColor;
    }
}
