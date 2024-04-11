using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 startPosXY, startPosXZ, startPosYZ;
    private Quaternion startRotXY, startRotXZ, startRotYZ;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject PlayerXY, PlayerXZ, PlayerYZ;
    [SerializeField] private GameObject gameMaster;
    public string startCam;
    void Start()
    {
        startPosXY = PlayerXY.transform.position;
        startRotXY = PlayerXY.transform.rotation;
        startPosXZ = PlayerXZ.transform.position;
        startRotXZ = PlayerXZ.transform.rotation;
        startPosYZ = PlayerYZ.transform.position;
        startRotYZ = PlayerYZ.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -20 && transform.name == "Player" + startCam)
        {
            DeathActivate();
        }
    }
    public void DeathActivate()
    {
        CameraChange.CamDir = startCam;
        if (startCam == "XY")
        {
            gameMaster.GetComponent<UIHandler>().SetCam(camXY);
            PlayerXZ.GetComponent<Movement>().enabled = false;
            PlayerYZ.GetComponent<Movement>().enabled = false;
            PlayerXZ.GetComponent<Rigidbody>().useGravity = false;
            PlayerYZ.GetComponent<Rigidbody>().useGravity = false;
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            PlayerXY.GetComponent<Movement>().dir = startCam;
            PlayerXY.transform.position = startPosXY;
            PlayerXY.transform.rotation = startRotXY;
            PlayerXY.GetComponent<Movement>().enabled = true;
            PlayerXY.GetComponent<Rigidbody>().useGravity = true;
            PlayerXY.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else if (startCam == "XZ")
        {
            gameMaster.GetComponent<UIHandler>().SetCam(camXZ);
            PlayerXY.GetComponent<Movement>().enabled = false;
            PlayerYZ.GetComponent<Movement>().enabled = false;
            PlayerXY.GetComponent<Rigidbody>().useGravity = false;
            PlayerYZ.GetComponent<Rigidbody>().useGravity = false;
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            PlayerXZ.GetComponent<Movement>().dir = startCam;
            PlayerXZ.transform.position = startPosXZ;
            PlayerXZ.transform.rotation = startRotXZ;
            PlayerXZ.GetComponent<Movement>().enabled = true;
            PlayerXZ.GetComponent<Rigidbody>().useGravity = true;
            PlayerXZ.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        if (startCam == "YZ")
        {
            gameMaster.GetComponent<UIHandler>().SetCam(camYZ);
            PlayerXZ.GetComponent<Movement>().enabled = false;
            PlayerXY.GetComponent<Movement>().enabled = false;
            PlayerXY.GetComponent<Rigidbody>().useGravity = false;
            PlayerXZ.GetComponent<Rigidbody>().useGravity = false;
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            PlayerYZ.GetComponent<Movement>().dir = startCam;
            PlayerYZ.transform.position = startPosYZ;
            PlayerYZ.transform.rotation = startRotYZ;
            PlayerYZ.GetComponent<Movement>().enabled = true;
            PlayerYZ.GetComponent<Rigidbody>().useGravity = true;
            PlayerYZ.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
