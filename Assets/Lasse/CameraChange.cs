using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public string CamDir;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    void Update()
    {
        if (Input.GetKey("1"))
        {
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
        }
        else if (Input.GetKey("2"))
        {
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
        }
        else if (Input.GetKey("3"))
        {
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
        }
    }
}
