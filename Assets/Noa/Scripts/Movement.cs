using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float speed;
    public float baseSpeed;
    [SerializeField] private float jumpForce;
    public Animator animator;
    private Rigidbody rb;
    private bool onGround;
    
    public string dir;

    private Ray ray;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
 
        ray = new Ray(transform.position, -transform.up);
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        if (Physics.Raycast(ray, 1.6f)) 
        {
            onGround = true;
            Debug.Log("Grounded");
        } else
        {
            onGround = false;
            Debug.Log("Flying");
        }
        if (!onGround)
        {
            speed = baseSpeed / 1.2f;
        }
        else speed = baseSpeed;

        if (dir == "XY")
        {
            if (Input.GetKey("a"))
            {
                transform.rotation = new Quaternion(0, -0.70711f, 0, 0.70711f);
                rb.velocity = new Vector3(-speed, rb.velocity.y, rb.velocity.z);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey("d"))
            {
                transform.rotation = new Quaternion(0, 0.70711f, 0, 0.70711f);
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey(KeyCode.Space) && onGround == true)
            {
                rb.velocity = transform.up * jumpForce;
                onGround = false;
            }

            if ((!(Input.GetKey("a") || Input.GetKey("d"))) && onGround == true)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetBool("Running", false);
            }
            else if ((!(Input.GetKey("a") || Input.GetKey("d"))) && onGround != true) 
            {
                rb.velocity = new Vector3(rb.velocity.x * 0.96f , rb.velocity.y, rb.velocity.z * 0.96f);
                animator.SetBool("Running", false);
            }
        }
        
        if (dir == "XZ")
        {
            onGround = true;
            if (Input.GetKey("a"))
            {
                transform.rotation = new Quaternion(0, -0.70711f, 0, 0.70711f);
                rb.velocity = new Vector3(-speed, rb.velocity.y, 0);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey("d"))
            {
                transform.rotation = new Quaternion(0, 0.70711f, 0, 0.70711f);
                rb.velocity = new Vector3(speed, rb.velocity.y, 0);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey("s"))
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
                rb.velocity = new Vector3(0, rb.velocity.y, -speed);
                animator.SetBool("Running", true);
            }

            if (Input.GetKey("w"))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
                rb.velocity = new Vector3(0, rb.velocity.y, speed);
                animator.SetBool("Running", true);
            }
            if ((!(Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("w") || Input.GetKey("s"))) && onGround == true)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetBool("Running", false);
            }
        }

        if (dir == "ZY")
        {
            if (Input.GetKey("a"))
            {
                transform.rotation = new Quaternion(0, 1, 0, 0);
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey("d"))
            {
                transform.rotation = new Quaternion(0, 0, 0, 1);
                rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -speed);
                animator.SetBool("Running", true);
            }
            if (Input.GetKey(KeyCode.Space) && onGround == true)
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                onGround = false;
            }

            if ((!(Input.GetKey("a") || Input.GetKey("d"))) && onGround == true)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                animator.SetBool("Running", false);
            }
            else if ((!(Input.GetKey("a") || Input.GetKey("d"))) && onGround != true)
            {
                rb.velocity = new Vector3(rb.velocity.x * 0.96f, rb.velocity.y, rb.velocity.z * 0.96f);
                animator.SetBool("Running", false);
            }
        }
    }


    public void SetDir(string direction)
    {
        dir = direction;
    }
}
