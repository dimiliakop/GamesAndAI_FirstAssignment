using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockCameraHandler : MonoBehaviour
{

    public GameObject Manager;
    // Start is called before the first frame update
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main; // Assign the main camera

        InitializeVariables();

    }
    private void InitializeVariables()
    {
        Vector2 cameraSize =  Manager.GetComponent<FlockManager>().goalPosLimits;
        mainCamera.orthographicSize = cameraSize.y + cameraSize.x; // Set the camera's size

    }

}
