using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour 
{
    private double xRotationRadians;

    // ground movement
    public float speed;
    private float ySpeed;
    private float zSpeed;

    // jumping
    private Vector3 jump;
    public float jumpForce;
    public float airSpeedMod;
    private bool isGrounded;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        getSpeed();
        getJump();
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Map") {
            isGrounded = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handles player ground movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (isGrounded) {
            gameObject.transform.position = new Vector3(transform.position.x + (horizontal * speed), transform.position.y + (vertical * ySpeed), transform.position.z + (vertical * zSpeed));
        }
        else {
            gameObject.transform.position = new Vector3(transform.position.x + (horizontal * (speed * airSpeedMod)), transform.position.y + (vertical * (ySpeed * airSpeedMod)), transform.position.z + (vertical * (zSpeed * airSpeedMod)));
        }
        
        // Handles player jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void getSpeed() 
    {
        double xRotation = (double)transform.rotation.x;
        xRotationRadians = ((xRotation * Math.PI) / 180);

        ySpeed = (float)(speed * Math.Sin(xRotationRadians));
        zSpeed = (float)(speed * Math.Cos(xRotationRadians));
    }

    void getJump() 
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, (float)(jumpForce / Math.Sin(xRotationRadians)), (float)(jumpForce / Math.Cos(xRotationRadians)));
    }
}
