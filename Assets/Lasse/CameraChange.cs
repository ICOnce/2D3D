using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private string CamDir;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject playah;
    Ray ray;
    float maxDistance = 1000;
    private void Start()
    {
        ray = new Ray(transform.position, transform.forward);
    }
    void Update()
    {
        if (Input.GetKey("1") && CamDir != "camXY")
        {
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            playah.GetComponent<Movement>().dir = "XY";
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            playah.GetComponent<Transform>().position = new Vector3(playah.GetComponent<Transform>().position.x, 500, playah.GetComponent<Transform>().position.z);
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3 (prevData.localScale.x, prevData.localScale.y, 500);
            }
            ray = new Ray(playah.transform.position,new Vector3(0, -1, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                Debug.Log("hit!");
                Transform temp = hit.collider.gameObject.transform;
                playah.transform.position = new Vector3(playah.transform.position.x, temp.position.y+(0.5f*temp.localScale.y)+(0.51f*playah.transform.localScale.y), playah.transform.position.z);
                Debug.Log(temp.name);
            }
        }
        else if (Input.GetKey("2") && CamDir != "camXZ")
        {
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
            playah.GetComponent<Movement>().dir = "XZ";
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().localScale = new Vector3(prevData.localScale.x, 1, prevData.localScale.z);
                Platform.GetComponent<Transform>().position = new Vector3(prevData.position.x, 0, prevData.position.z);
            }
        }
        else if (Input.GetKey("3") && CamDir != "camYZ")
        {
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            playah.GetComponent<Movement>().dir = "ZY";
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3(500, prevData.localScale.y, prevData.localScale.z);
                playah.GetComponent<Transform>().position = new Vector3 (playah.GetComponent<Transform>().position.x, 1.5f, playah.GetComponent<Transform>().position.z);
            }
        }
    }
    void CheckForColliders()
    {

    }
}
