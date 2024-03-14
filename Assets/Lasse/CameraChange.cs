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
    Ray ray, rayD1, rayD2, rayD3, rayD4, rayW1, rayW2, rayE1, rayE2, rayN1, rayN2, rayS1, rayS2, rayU1, rayU2, rayU3, rayU4;
    float maxDistance = 1000;
    private Vector3 Down = new Vector3(0, -1, 0), West = new Vector3(0, 0, 1), East = new Vector3(0, 0, -1), Up = new Vector3(0, 1, 0), North = new Vector3(1, 0, 0), South = new Vector3(-1,0,0);
    public Transform fakePlayer;
    private string PrevDir;
    private Vector3 NE = new Vector3(0.5f,-0.99f,0.5f), NW = new Vector3(0.5f, -0.99f, -0.5f), SE = new Vector3(-0.5f, -0.99f, 0.5f), SW = new Vector3(-0.5f, -0.99f, -0.5f);
    private void Start()
    {

    }
    void Update()
    {
        PrevDir = CamDir;
        fakePlayer.transform.position = playah.transform.position + new Vector3(500, 0, 500);
        if (Input.GetKeyDown("1") && CamDir != "camXY")
        {
            Transform PlayerTemp = playah.transform;
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "XY") == false) return;
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            playah.GetComponent<Movement>().dir = "XY";
            CameraMovement.ActiveCam = "XY";
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            if (PrevDir == "camXZ") playah.GetComponent<Transform>().position = new Vector3(playah.GetComponent<Transform>().position.x, 500, playah.GetComponent<Transform>().position.z);
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3 (prevData.localScale.x, prevData.localScale.y, 500);
            }
            if (PrevDir == "camXZ") GetTheHeight();
        }
        else if (Input.GetKeyDown("2") && CamDir != "camXZ")
        {
            Transform PlayerTemp = playah.transform;
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "XZ") == false) return;
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
            playah.GetComponent<Movement>().dir = "XZ";
            CameraMovement.ActiveCam = "XZ";
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
            if (CheckForCollider(PlayerTemp.position + new Vector3(500, 0, 500), "YZ") == false) return;
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            playah.GetComponent<Movement>().dir = "ZY";
            CameraMovement.ActiveCam = "YZ";
            if (PrevDir == "camXZ") playah.GetComponent<Transform>().position = new Vector3(playah.GetComponent<Transform>().position.x, 500, playah.GetComponent<Transform>().position.z);
            GameObject[] PlatList = GameObject.FindGameObjectsWithTag("Platform");
            foreach (GameObject Platform in PlatList)
            {
                Transform prevData = Platform.GetComponent<PlatData>().prevTrans;
                Platform.GetComponent<Transform>().localScale = new Vector3(Platform.GetComponent<PlatData>().scalX, Platform.GetComponent<PlatData>().scalY, Platform.GetComponent<PlatData>().scalZ);
                Platform.GetComponent<Transform>().position = new Vector3(Platform.GetComponent<PlatData>().cordX, Platform.GetComponent<PlatData>().cordY, Platform.GetComponent<PlatData>().cordZ);
                Platform.GetComponent<Transform>().localScale = new Vector3(500, prevData.localScale.y, prevData.localScale.z);
            }
            if (PrevDir == "camXZ") GetTheHeight();
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
            rayD1 = new Ray(X + NE, Down);
            rayD2 = new Ray(X + NW, Down);
            rayD3 = new Ray(X + SE, Down);
            rayD4 = new Ray(X + SW, Down);
            rayE1 = new Ray(X + SE, East);
            rayE2 = new Ray(X + NE, East);
            rayW1 = new Ray(X + SW, West);
            rayW2 = new Ray(X + NW, West);
            if ((Physics.Raycast(rayD1) || Physics.Raycast(rayD2) || Physics.Raycast(rayD3) || Physics.Raycast(rayD4)) && !Physics.Raycast(rayE1) && !Physics.Raycast(rayE2) && !Physics.Raycast(rayW1) && !Physics.Raycast(rayW2))
            {               
                return true;
            }
            else if (Physics.Raycast(rayE1) || Physics.Raycast(rayW1))
            {
                return false;
            }
            if (CamDir == "camYZ")
            {
                X += new Vector3(0, -1.5f, 0);
                rayE1 = new Ray(X + SE, East);
                rayE2 = new Ray(X + NE, East);
                rayW1 = new Ray(X + SW, West);
                rayW2 = new Ray(X + NW, West);
                if (Physics.Raycast(rayE1) || Physics.Raycast(rayE2) || Physics.Raycast(rayW1) || Physics.Raycast(rayW2))
                {
                    return true;
                }
            }
        }
        if (newCamDir == "XZ")
        {
            rayD1 = new Ray(X + NE, Down);
            rayD2 = new Ray(X + NW, Down);
            rayD3 = new Ray(X + SE, Down);
            rayD4 = new Ray(X + SW, Down);
            rayU1 = new Ray(X + NE, Up);
            rayU2 = new Ray(X + NW, Up);
            rayU3 = new Ray(X + SE, Up);
            rayU4 = new Ray(X + SW, Up);
            if ((Physics.Raycast(rayD1) || Physics.Raycast(rayD2) || Physics.Raycast(rayD3) || Physics.Raycast(rayD4)) && !Physics.Raycast(rayU1) && !Physics.Raycast(rayU2) && !Physics.Raycast(rayU3) && !Physics.Raycast(rayU4))
            {
                return true;
            }
        }
        if (newCamDir == "YZ")
        {
            rayD1 = new Ray(X + NE, Down);
            rayD2 = new Ray(X + NW, Down);
            rayD3 = new Ray(X + SE, Down);
            rayD4 = new Ray(X + SW, Down);
            rayN1 = new Ray(X + NE, North);
            rayN2 = new Ray(X + NW, North);
            rayS1 = new Ray(X + SE, South);
            rayS2 = new Ray(X + SW, South);
            if ((Physics.Raycast(rayD1) || Physics.Raycast(rayD2) || Physics.Raycast(rayD3) || Physics.Raycast(rayD4)) && !Physics.Raycast(rayN1) && !Physics.Raycast(rayN2) && !Physics.Raycast(rayS1) && !Physics.Raycast(rayS2))
            {
                return true;
            }
            else if (Physics.Raycast(rayN1) || Physics.Raycast(rayN2) || Physics.Raycast(rayS1) || Physics.Raycast(rayS2))
            {
                return false;
            }
            if (CamDir == "camXY")
            {
                X += new Vector3(0, -1.5f, 0);
                rayN1 = new Ray(X + NE, North);
                rayN2 = new Ray(X + NW, North);
                rayS1 = new Ray(X + SE, South);
                rayS2 = new Ray(X + SW, South);
                if (Physics.Raycast(rayN1) || Physics.Raycast(rayN2) || Physics.Raycast(rayS1) || Physics.Raycast(rayS2))
                {
                    return true;
                }
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