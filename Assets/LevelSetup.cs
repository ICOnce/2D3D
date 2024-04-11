using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour

{
    //Setting the colors for the levels, for less clutter later
    private Color XY = new Color(1, 0, 0);
    private Color YZ = new Color(0, 1, 0);
    private Color XZ = new Color(0, 0, 1);

    //Setting private variables with SerializeField so they can be set in the editor
    //Player refers 
    [SerializeField] private GameObject startPlayer, LevelHolder;
    [SerializeField] private string StartLevel;

    void Start()
    {
        //Generating the three levels
        GameObject LevelXY = Instantiate(LevelHolder);
        GameObject LevelYZ = Instantiate(LevelHolder);
        GameObject LevelXZ = Instantiate(LevelHolder);

        //Setting the position of the three levels
        LevelXY.transform.position = new Vector3(500, 0, 0);
        LevelYZ.transform.position = new Vector3(0, 0, 500);
        LevelXZ.transform.position = new Vector3(-500, 0, 0);

        //For loops to set the scale & colour of the three levels
        foreach (Transform child in LevelXY.transform)
        {
            child.transform.localScale = new Vector3(500, child.transform.localScale.y, child.transform.localScale.z);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = XY;
            else child.GetChild(0).GetComponent<Renderer>().material.color = XY;
            if (child.tag == "PlatformXZ")
            {
                child.GetComponent<Renderer>().material.color = XZ;
                child.GetComponent<BoxCollider>().enabled = false;
            }
            if (child.tag == "PlatformYZ")
            {
                child.GetComponent<Renderer>().material.color = YZ;
                child.GetComponent<BoxCollider>().enabled = false;
            }
        }
        foreach (Transform child in LevelYZ.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 500);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = YZ;
            else child.GetChild(0).GetComponent<Renderer>().material.color = YZ;
            if (child.tag == "PlatformXZ")
            {
                child.GetComponent<Renderer>().material.color = XZ;
                child.GetComponent<BoxCollider>().enabled = false;
            }
            if (child.tag == "PlatformXY")
            {
                child.GetComponent<Renderer>().material.color = XY;
                child.GetComponent<BoxCollider>().enabled = false;
            }
        }
        foreach (Transform child in LevelXZ.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, 1, child.transform.localScale.z);
            child.transform.position = new Vector3(child.transform.position.x, 0, child.transform.position.z);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = XZ;
            else child.GetChild(0).GetComponent<Renderer>().material.color = XZ;
            if (child.tag == "PlatformXY")
            {
                child.GetComponent<Renderer>().material.color = XY;
                child.GetComponent<BoxCollider>().enabled = false;
            }
            if (child.tag == "PlatformYZ")
            {
                child.GetComponent<Renderer>().material.color = YZ;
                child.GetComponent<BoxCollider>().enabled = false;
            }
        }

        //setting some start parameters in other scripts
        CameraChange.CamDir = "cam" + StartLevel;
        startPlayer.GetComponent<Movement>().dir = StartLevel;
        CameraMovement.ActiveCam = StartLevel;
    }
}
