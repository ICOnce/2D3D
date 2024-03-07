using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraChange : MonoBehaviour
{
    private string CamDir;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    [SerializeField] private GameObject playah;
    Ray ray, rayD, rayW, rayE, rayN, rayS, rayU;
    float maxDistance = 1000;
    private Vector3 Down = new Vector3(0, -1, 0), West = new Vector3(0, 0, 1), East = new Vector3(0, 0, -1), Up = new Vector3(0, 1, 0), North = new Vector3(1, 0, 0), South = new Vector3(-1,0,0);
    public Transform fakePlayer;
    private string PrevDir;
    private void Start()
    {

    }
    void Update()
    {
        PrevDir = CamDir;
        fakePlayer.transform.position = playah.transform.position + new Vector3(500, 0, 500);
        camXY.GetComponent<Transform>().position = new Vector3(playah.transform.position.x, playah.transform.position.y, -500);
        camXZ.GetComponent<Transform>().position = new Vector3(playah.transform.position.x, 500, playah.transform.position.z);
        camYZ.GetComponent<Transform>().position = new Vector3(500, playah.transform.position.y, playah.transform.position.z);
        if (Input.GetKeyDown("1") && CamDir != "camXY")
        {
            Transform PlayerTemp = playah.transform;
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "XY") == false)
            {
                return;
            }
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            if (PrevDir == "camXZ") playah.GetComponent<Transform>().position = new Vector3(playah.GetComponent<Transform>().position.x, 500, playah.GetComponent<Transform>().position.z);
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3 (prevData.localScale.x, prevData.localScale.y, 500);
            }
            Invoke("GetTheHeight", 0.05f) ;
        }
        else if (Input.GetKeyDown("2") && CamDir != "camXZ")
        {
            Transform PlayerTemp = playah.transform;
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "XZ") == false)
            {
                return;
            }
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
                Platform.GetComponent<Transform>().position = new Vector3(prevData.position.x, playah.transform.position.y-1, prevData.position.z);
            }
        }
        else if (Input.GetKeyDown("3") && CamDir != "camYZ")
        {
            Transform PlayerTemp = playah.transform;
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "YZ") == false)
            {   
                return;
            }
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            playah.GetComponent<Movement>().dir = "ZY";
            if (PrevDir == "camXZ") playah.GetComponent<Transform>().position = new Vector3(playah.GetComponent<Transform>().position.x, 500, playah.GetComponent<Transform>().position.z);
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3(500, prevData.localScale.y, prevData.localScale.z);
            }
            if (PrevDir == "camXZ") Invoke("GetTheHeight", 0.05f);
        }
    }
    private bool CheckForCollider(Vector3 X, string newCamDir)
    {
        if (CamDir == "camXZ")
        {
            ray = new Ray(X + new Vector3(0,500,0), Down);
            if (Physics.Raycast(ray, out RaycastHit hit)) X = hit.point + new Vector3(0,1,0);
        }
        if (newCamDir == "XY")
        {
            rayD = new Ray(X, Down);
            rayE = new Ray(X, East);
            rayW = new Ray(X, West);
            if (Physics.Raycast(rayD) && !Physics.Raycast(rayE) && !Physics.Raycast(rayW))
            {
                return true;
            }
        }
        if (newCamDir == "XZ")
        {
            rayD = new Ray(X, Down);
            rayU = new Ray(X, Up);
            if (Physics.Raycast(rayD) && !Physics.Raycast(rayU))
            {
                return true;
            }
        }
        if (newCamDir == "YZ")
        {
            rayD = new Ray(X, Down);
            rayN = new Ray(X, North);
            rayS = new Ray(X, South);
            if (Physics.Raycast(rayD) && !Physics.Raycast(rayN) && !Physics.Raycast(rayS)) {
                return true;
            }
        }
        return false;
    }
    private void GetTheHeight()
    {
        ray = new Ray(playah.transform.position + new Vector3 (500, 0, 500), new Vector3(0, -1, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Transform temp = hit.collider.gameObject.transform;
            playah.transform.position = new Vector3(playah.transform.position.x, temp.position.y + (0.5f * temp.localScale.y) + (0.6f * playah.transform.localScale.y), playah.transform.position.z);
        }
    }
}