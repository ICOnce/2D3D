using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private float speed;
    public float baseSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int Level;
    [SerializeField] private Transform PlayerXY, PlayerYZ, PlayerXZ;
    public Animator animator;
    private Rigidbody rb;
    public bool onGround;
    
    public string dir;
    
    private Ray ray;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
 
        ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out RaycastHit hit, 1.6f)) 
        {
            onGround = true;
            if (hit.transform.tag == "Winner")
            {
                SceneManager.LoadScene("Level" + (Level+1));
            }
            if (hit.transform.tag == "Spoke")
            {
                transform.GetComponent<Death>().DeathActivate();
            }
        } else
        {
            onGround = false;
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

        if (dir == "YZ")
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
