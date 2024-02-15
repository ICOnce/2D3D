using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float gravity = 2f;
    void Update()
    {
        if (Input.GetKey("a")) transform.position = transform.position - new Vector3(1, 0, 0) * speed * Time.deltaTime;
        if (Input.GetKey("d")) transform.position = transform.position + new Vector3(1, 0, 0) * speed * Time.deltaTime;
        transform.position = transform.position - new Vector3(0, gravity, 0) * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision other)
    {
        transform.position = transform.position + new Vector3(0,(transform.position.y-other.transform.position.y)/ 2, 0);
    }
}
