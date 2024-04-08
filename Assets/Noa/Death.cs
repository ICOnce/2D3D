using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPos;
    private Quaternion startRot;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject PlayerXY, PlayerXZ, PlayerYZ;
    [SerializeField] string startCam;
    void Start()
    {
        startPos = transform.position;
        startRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20 && transform.name == "Player" + startCam)
        {
            CameraChange.CamDir = startCam;
            transform.GetComponent<Movement>().dir = startCam;
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            transform.position = startPos;
            transform.rotation = startRot;
            transform.GetComponent<Movement>().enabled = true;
            if (startCam == "XY")
            {
                PlayerXZ.GetComponent<Movement>().enabled = false;
                PlayerYZ.GetComponent<Movement>().enabled = false;
            }
            else if (startCam == "XZ")
            {
                PlayerXY.GetComponent<Movement>().enabled = false;
                PlayerYZ.GetComponent<Movement>().enabled = false;
            }
            if (startCam == "YZ")
            {
                PlayerXZ.GetComponent<Movement>().enabled = false;
                PlayerXY.GetComponent<Movement>().enabled = false;
            }
        }
    }
}
