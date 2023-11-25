using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekAndFleeCameraHandler : MonoBehaviour
{

    public GameObject targetParticle;
    // Start is called before the first frame update
    void Start()
    {
        InitializeVariables();

    }
    private void InitializeVariables()
    {
        TargetParticleMovement targetParticleComponent = targetParticle.GetComponent<TargetParticleMovement>();
        float distanceFromCenter = targetParticleComponent.distanceFromCenter;

        Camera cameraComponent = GetComponent<Camera>();   // Get the Camera component
        cameraComponent.orthographicSize = distanceFromCenter * 2; // Change the size of the camera
    }

}
