using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody rb;
    private bool onGround;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        Debug.Log("Grounded = " + onGround);
        if (Input.GetKey("a")) 
        {
            transform.rotation = new Quaternion(0, -0.70711f, 0, 0.70711f);
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey("d")) 
        {
            transform.rotation = new Quaternion(0, 0.70711f, 0, 0.70711f);
            rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.Space) && onGround == true)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
            onGround = false;
        }

        if ((!(Input.GetKey("a") || Input.GetKey("d"))) && onGround == true)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.position.y+1 < transform.position.y)
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision) 
    {
        onGround = false; 
    }
}
