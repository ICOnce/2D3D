using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatData : MonoBehaviour
{
    public Transform prevTrans;
    public float cordX, cordY, cordZ, scalX, scalY, scalZ;
    // Start is called before the first frame update
    void Start()
    {
        prevTrans = transform;
        cordX= transform.position.x;
        cordY= transform.position.y;
        cordZ= transform.position.z;
        scalX= transform.localScale.x;
        scalY= transform.localScale.y;
        scalZ= transform.localScale.z; 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
