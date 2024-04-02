using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
   
{ 
    public GameObject player, LevelHolder;
    void Start()
    {
        GameObject LevelXY = Instantiate(LevelHolder);
        GameObject LevelYZ = Instantiate(LevelHolder);
        GameObject LevelXZ = Instantiate(LevelHolder);
        LevelXY.transform.position = new Vector3(500, 0, 0);
        LevelYZ.transform.position = new Vector3(0, 0, 500);
        LevelXZ.transform.position = new Vector3(-500, 0, 0);
        foreach (Transform child in LevelXY.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, child.transform.localScale.y, 500);
        }
        foreach (Transform child in LevelYZ.transform)
        {
            child.transform.localScale = new Vector3(500, child.transform.localScale.y, child.transform.localScale.z);
        }
        foreach (Transform child in LevelXZ.transform)
        {
            child.transform.localScale = new Vector3(child.transform.localScale.x, 1, child.transform.localScale.z);
            child.transform.position = new Vector3(child.transform.position.x, 0, child.transform.position.z);
        }
        //StartLevelCamPos();
    }
    void Update()
    {
        
    }
    void StartLevelCamPos()
    {
        CameraChange.CamDir = "camXY";
        player.GetComponent<Movement>().dir = "XY";
        CameraMovement.ActiveCam = "XY";
        GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
        foreach (GameObject Platform in PlatList)
        {
            Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
            Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
            Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
            Platform.GetComponent<Transform>().localScale = new Vector3(prevData.localScale.x, prevData.localScale.y, 500);
        }
    }
}
