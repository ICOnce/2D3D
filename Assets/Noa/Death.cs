using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    // Store start position of players
    private Vector3 startPosXY, startPosXZ, startPosYZ;
    // Store start rotation of players
    private Quaternion startRotXY, startRotXZ, startRotYZ;
    // Variables that need to be assigned in the editor, but inaccessible to other scripts
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject PlayerXY, PlayerXZ, PlayerYZ;
    [SerializeField] private GameObject gameMaster;
    //Variable to set the startCam in editor
    public string startCam;
    void Start()
    {
        // Set values for start positions and rotations
        startPosXY = PlayerXY.transform.position;
        startRotXY = PlayerXY.transform.rotation;
        startPosXZ = PlayerXZ.transform.position;
        startRotXZ = PlayerXZ.transform.rotation;
        startPosYZ = PlayerYZ.transform.position;
        startRotYZ = PlayerYZ.transform.rotation;
    }
    void Update()
    {
        // Check if player has fallen far enough to die
        if (transform.position.y < -20 && transform.name == "Player" + startCam)
        {
            DeathActivate();
        }
    }
    public void DeathActivate()
    {
        // Set camera direction from CameraChange script to startCam
        CameraChange.CamDir = startCam;

        // If statement to check which startCam you have, to make sure you're set back to the correct one
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
            gameMaster.GetComponent<CameraChange>().currentPlayer = PlayerXY.transform;
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
            gameMaster.GetComponent<CameraChange>().currentPlayer = PlayerXZ.transform;
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
            gameMaster.GetComponent<CameraChange>().currentPlayer = PlayerYZ.transform;
        }
    }
}
