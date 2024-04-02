using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FakeLevel : MonoBehaviour
{
    void Start()
    {
        GameObject FakeLevel = Instantiate(gameObject);
        FakeLevel.transform.position = gameObject.transform.position + new Vector3(500,0,500);
        
    }
    void Update()
    {
        
    }
}
