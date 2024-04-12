using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSetup : MonoBehaviour

{
    //Setting the colors for the levels, for less clutter later
    private Color YZ = new Color(1, 0, 0, 1f);
    private Color XY = new Color(0, 1, 0, 1f);
    private Color XZ = new Color(0, 0, 1, 1f);

    [SerializeField] private Material matXY;
    [SerializeField] private Material matXZ;
    [SerializeField] private Material matYZ;

    //Setting private variables with SerializeField so they can be set in the editor
    [SerializeField] private GameObject startPlayer, LevelHolder;
    [SerializeField] private string StartLevel;

    void Start()
    {
        //Generating the three levels
        GameObject LevelXY = Instantiate(LevelHolder);
        GameObject LevelYZ = Instantiate(LevelHolder);
        GameObject LevelXZ = Instantiate(LevelHolder);

        //Setting the position of the three levels
        LevelYZ.transform.position = new Vector3(500, 0, 0);
        LevelXY.transform.position = new Vector3(0, 0, 500);
        LevelXZ.transform.position = new Vector3(-500, 0, 0);

        //Three foreach loops to set the scale & colour of the three levels
        foreach (Transform child in LevelYZ.transform)
        {
            //Set the general scale to 500 on the ignored axis
            child.transform.localScale = new Vector3(500, child.transform.localScale.y, child.transform.localScale.z);

            //Change the color of the platforms that can be stood on
            if (child.tag == "Platform" || child.tag == "PlatformYZ") child.GetComponent<Renderer>().material.color = YZ;

            //Get the child of "Winner" as "Winner" is an Empty
            else if (child.tag == "Winner" || child.tag == "Spoke") child.GetChild(0).GetComponent<Renderer>().material.color = YZ;

            //Set color, collision, and smaller stretch on platforms that can not be seen from XY
            else if (child.tag == "PlatformXZ")
            {
                child.GetComponent<Renderer>().material =  matXZ;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(200, child.transform.localScale.y, child.transform.localScale.z);
            }
            else if (child.tag == "PlatformXY")
            {
                child.GetComponent<Renderer>().material = matXY;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(200, child.transform.localScale.y, child.transform.localScale.z);
            }
        }
        //Repeat above foreach loop
        foreach (Transform child in LevelXY.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 500);
            if (child.tag == "Platform" || child.tag == "PlatformXY") child.GetComponent<Renderer>().material.color = XY;
            else if (child.tag == "Winner" || child.tag == "Spoke") child.GetChild(0).GetComponent<Renderer>().material.color = XY;
            if (child.tag == "PlatformXZ")
            {
                child.GetComponent<Renderer>().material = matXZ;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 200);
            }
            if (child.tag == "PlatformYZ")
            {
                child.GetComponent<Renderer>().material = matYZ;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 200);
            }
        }
        //Repeat above foreach loop
        foreach (Transform child in LevelXZ.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, 1, child.transform.localScale.z);
            child.transform.position = new Vector3(child.transform.position.x, 0, child.transform.position.z);
            if (child.tag == "Platform" || child.tag == "PlatformXZ") child.GetComponent<Renderer>().material.color = XZ;
            else if (child.tag == "Winner" || child.tag == "Spoke") child.GetChild(0).GetComponent<Renderer>().material.color = XZ;
            if (child.tag == "PlatformXY")
            {
                child.GetComponent<Renderer>().material = matXY;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(child.transform.localScale.x, 0.1f, child.transform.localScale.z);
            }
            if (child.tag == "PlatformYZ")
            {
                child.GetComponent<Renderer>().material = matYZ;
                child.GetComponent<BoxCollider>().enabled = false;
                child.transform.localScale = new Vector3(child.transform.localScale.x, 0.1f, child.transform.localScale.z);
            }
        }

        //setting some start parameters in other scripts
        CameraChange.CamDir = "cam" + StartLevel;
        startPlayer.GetComponent<Movement>().dir = StartLevel;
        CameraMovement.ActiveCam = StartLevel;
    }
}