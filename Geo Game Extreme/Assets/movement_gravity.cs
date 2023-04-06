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

    public float speed = 2;

    void Update()
    {
        //float horizontalInput= Input.GetAxis("Horizontal");
        //float verticalInput= Input.GetAxis("Vertical");
        //Vector3 movementDirection=new Vector3(horizontalInput,0,verticalInput);
        //movementDirection.Normalize();
        //transform.Translate(movementDirection*speed*Time.deltaTime);
        //if (movementDirection != Vector3.zero)
        //{
            //transform.forward=movementDirection;
        //}
    
        float z = Input.GetAxis("Horizontal");
        float x = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(-x, 0, z);
        movement = Vector3.ClampMagnitude(movement, 1);
        transform.Translate(movement * speed * Time.deltaTime);

    }
    void FixedUpdate()
    {
        ProcessGravity();
    }


    void ProcessGravity()
    {
        Vector3 diff= transform.position-gravityTarget.position;
        rb.AddForce(-diff.normalized*gravity*(rb.mass));
        
        if(autoOrient1)
        {
            AutoOrient(-diff);
        }
    }
    
    void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection=Quaternion.FromToRotation(-transform.up,down)*transform.rotation;
        transform.rotation=Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed*Time.deltaTime);
    }
}
