using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
   
{ 
    public GameObject player, LevelHolder;
    private Color XY = new Color(1, 0, 0);
    private Color YZ = new Color(0, 1, 0);
    private Color XZ = new Color(0, 0, 1);
    [SerializeField] private string StartLevel;
    public int Index;

    void Start()
    {
        GameObject LevelXY = Instantiate(LevelHolder);
        GameObject LevelYZ = Instantiate(LevelHolder);
        GameObject LevelXZ = Instantiate(LevelHolder);
        LevelXY.name = "LevelXY";
        LevelYZ.name = "LevelYZ";
        LevelXZ.name = "LevelXZ";
        LevelYZ.transform.position = new Vector3(500, 0, 0);
        LevelXY.transform.position = new Vector3(0, 0, 500);
        LevelXZ.transform.position = new Vector3(-500, 0, 0);
        foreach (Transform child in LevelYZ.transform)
        {
            child.transform.localScale = new Vector3(500, child.transform.localScale.y, child.transform.localScale.z);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = XY;
            else child.GetChild(0).GetComponent<Renderer>().material.color = XY;
        }
        foreach (Transform child in LevelXY.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 500);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = YZ;
            else child.GetChild(0).GetComponent<Renderer>().material.color = YZ;
        }
        foreach (Transform child in LevelXZ.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, 1, child.transform.localScale.z);
            child.transform.position = new Vector3(child.transform.position.x, 0, child.transform.position.z);
            if (child.tag == "Platform") child.GetComponent<Renderer>().material.color = XZ;
            else child.GetChild(0).GetComponent<Renderer>().material.color = XZ;
        }
        StartLevelCamPos();
    }
    void Update()
    {

    }
    void StartLevelCamPos()
    {
        CameraChange.CamDir = "cam" + StartLevel;
        player.GetComponent<Movement>().dir = StartLevel;
        CameraMovement.ActiveCam = StartLevel;
    }
}
