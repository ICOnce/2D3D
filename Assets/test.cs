using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, -1, 0.5f), new Vector3(0, -1, 0), Color.red);
        Debug.DrawRay(transform.position + new Vector3(0, -1, -0.5f), new Vector3(0, -1, 0), Color.red);
        Debug.DrawRay(transform.position + new Vector3(0.5f , -1, 0), new Vector3(0, -1, 0), Color.red);
        Debug.DrawRay(transform.position + new Vector3(-0.5f , -1, 0), new Vector3(0, -1, 0), Color.red);

    }
}
