using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPCameraView : MonoBehaviour
{

    private Animator anim;
    int IdleSimple;
    int FlyingFWD;
 

    private float zAxisInput;
    private float xAxisInput;
    private float speed = 10.0f;
    private float turnSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerDirection();
        
    }


    private void PlayerDirection()
    {
        float zAxisInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * zAxisInput, Space.World);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);



        }
    }
}