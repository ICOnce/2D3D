using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blergh : MonoBehaviour
{
    public GameObject LevelHolder;
    public Camera camXY, camXZ, camYZ;
    public GameObject player, playerXY, playerXZ, playerYZ;
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
    }

    void Update()
    {
        if (camXY.enabled == true)
        {
            playerYZ.transform.position = playerXY.transform.position + new Vector3(500, 0, -500);
            playerXZ.transform.position = playerXY.transform.position + new Vector3(-500, 0, -500);
        }
        if (camXZ.enabled == true)
        {
            playerYZ.transform.position = playerXZ.transform.position + new Vector3(1000, 0, 0);
            playerXY.transform.position = playerXZ.transform.position + new Vector3(500, 0, 500);
        }
        if (camYZ.enabled == true)
        {
            playerXY.transform.position = playerYZ.transform.position + new Vector3(-500, 0, 500);
            playerXZ.transform.position = playerYZ.transform.position + new Vector3(-1000, 0, 0);
        }



        if (Input.GetKeyDown("1"))
        {
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            playerXY.GetComponent<Movement>().enabled = true;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = false;
        }
        else if (Input.GetKeyDown("2"))
        {
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            player.GetComponent<Movement>().dir = "XZ";
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = true;
            playerYZ.GetComponent<Movement>().enabled = false;
        }
        else if (Input.GetKeyDown("3"))
        {
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            player.GetComponent<Movement>().dir = "ZY";
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = true;
        }
    }
}
