using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0,6,-60);
    
    // Update is called once per frame
    void Update()
    {

        
       transform.position = player.transform.TransformPoint(offset);
       transform.rotation = player.transform.rotation;
    }
}