using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement_gravity : MonoBehaviour
{
    public Transform gravityTarget;
    public float gravity= 9.81f;

    Rigidbody rb;
    public bool autoOrient1=false;
    public float autoOrientSpeed=1f;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    public float speed = 2f;
    public float rotation_speed = 100f;
    void Update()
    {
        

        transform.position+=-transform.right*speed*Time.deltaTime;
        
        if (Input.GetKey("d"))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotation_speed);
        }

        if (Input.GetKey("a"))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotation_speed);
        }

    }
    void FixedUpdate()
    {
        ProcessGravity();
    }


    void ProcessGravity()
    {
        Vector3 diff= transform.position-gravityTarget.position;
        rb.AddForce(-diff*gravity*(rb.mass));
        
        if(autoOrient1)
        {
            AutoOrient(-diff);
        }
    }
    
    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection=Quaternion.FromToRotation(-transform.up,down)*transform.rotation;
        transform.rotation=Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed*speed*Time.deltaTime);
    }
}
