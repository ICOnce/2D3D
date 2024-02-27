using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Rigidbody rb;
    private bool onGround;

    public string dir;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (dir == "XY")
        {
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
        
        if (dir == "XZ")
        {
            onGround = true;
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
            if (Input.GetKey("s"))
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }

            if (Input.GetKey("w"))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            if ((!(Input.GetKey("a") || Input.GetKey("d"))) || Input.GetKey("w") || Input.GetKey("s") && onGround == true)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }

        if (dir == "ZY")
        {
            if (Input.GetKey("a"))
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
                rb.AddForce(transform.forward * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey("d"))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
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

    public void SetDir(string direction)
    {
        dir = direction;
    }
}
