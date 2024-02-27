using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private string CamDir;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject playah;
    void Update()
    {
        if (Input.GetKey("1"))
        {
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            playah.GetComponent<Movement>().dir = "XY";
        }
        else if (Input.GetKey("2"))
        {
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
            playah.GetComponent<Movement>().dir = "XZ";
        }
        else if (Input.GetKey("3"))
        {
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            playah.GetComponent<Movement>().dir = "ZY";
        }
    }
}
