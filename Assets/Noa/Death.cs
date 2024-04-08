using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    Transform startPos;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    string startCam;
    void Start()
    {
        startPos.position = transform.position;
        startPos.rotation = transform.rotation;
        startCam = "XY";
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20)
        {
            CameraChange.CamDir = "XY";
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            transform.position = startPos.position;
            transform.rotation = startPos.rotation;
        }
    }
}
