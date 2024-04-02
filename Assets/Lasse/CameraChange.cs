using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraChange : MonoBehaviour
{
    public static string CamDir;
    [SerializeField] private Camera camXY, camXZ, camYZ;
    Ray ray, rayD1, rayD2, rayD3, rayD4, rayW1, rayW2, rayE1, rayE2, rayN1, rayN2, rayS1, rayS2, rayU1, rayU2, rayU3, rayU4;
    float maxDistance = 1000;
    private Vector3 Down = new Vector3(0, -1, 0), West = new Vector3(0, 0, 1), East = new Vector3(0, 0, -1), Up = new Vector3(0, 1, 0), North = new Vector3(1, 0, 0), South = new Vector3(-1,0,0);
    public Transform playerYZ, playerXZ, playerXY;
    private string PrevDir;
    private Vector3 NE = new Vector3(0.499f,-0.99f,0.499f), NW = new Vector3(0.499f, -0.99f, -0.499f), SE = new Vector3(-0.499f, -0.99f, 0.499f), SW = new Vector3(-0.499f, -0.99f, -0.499f);
    private void Start()
    {

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
        PrevDir = CamDir;
        if (Input.GetKeyDown("1") && CamDir != "camXY")
        {
            if (CheckForCollider(playerXY.position, "XY") == false) return;
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            playerXY.GetComponent<Movement>().enabled = true;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = false;
            CameraMovement.ActiveCam = "XY";
            if (PrevDir == "camXZ") GetTheHeight();
        }
        else if (Input.GetKeyDown("2") && CamDir != "camXZ")
        {
            if (CheckForCollider(playerXZ.position, "XZ") == false) return;
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = true;
            playerYZ.GetComponent<Movement>().enabled = false;
            CameraMovement.ActiveCam = "XZ";
        }
        else if (Input.GetKeyDown("3") && CamDir != "camYZ")
        {
            if (CheckForCollider(playerYZ.position, "YZ") == false) return;
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = true;
            CameraMovement.ActiveCam = "YZ";
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
        Debug.Log((X + SW) + " : " + (X + NW));
        if (newCamDir == "XZ")
        {
            rayD1 = new Ray(X + NE, Down);
            Debug.DrawRay(X + NE, Down, Color.red);
            rayD2 = new Ray(X + NW, Down);
            Debug.DrawRay(X + NW, Down, Color.red);
            rayD3 = new Ray(X + SE, Down);
            Debug.DrawRay(X + SE, Down, Color.red);
            rayD4 = new Ray(X + SW, Down);
            Debug.DrawRay(X + SW, Down, Color.red);
            rayU1 = new Ray(X + NE, Up);
            Debug.DrawRay(X + NE, Up, Color.red);
            rayU2 = new Ray(X + NW, Up);
            Debug.DrawRay(X + NW, Up, Color.red);
            rayU3 = new Ray(X + SE, Up);
            Debug.DrawRay(X + SE, Up, Color.red);
            rayU4 = new Ray(X + SW, Up);
            Debug.DrawRay(X + SW, Up, Color.red);
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
        float shortDist=Mathf.Infinity;
        Transform shortPlat;
        if (CamDir == "XY")
        {
            rayD1 = new Ray(playerXY.transform.position + NE, Down);
            rayD2 = new Ray(playerXY.transform.position + NW, Down);
            rayD3 = new Ray(playerXY.transform.position + SE, Down);
            rayD4 = new Ray(playerXY.transform.position + SW, Down);
            if (Physics.Raycast(rayD1, out RaycastHit hitXY1, maxDistance))
            {
                if (playerXY.transform.position.y - hitXY1.transform.position.y < shortDist)
                {
                    shortDist = playerXY.transform.position.y - hitXY1.transform.position.y;
                    shortPlat = hitXY1.transform;
                }
            }
            if (Physics.Raycast(rayD2, out RaycastHit hitXY2, maxDistance))
            {
                if (playerXY.transform.position.y - hitXY2.transform.position.y < shortDist)
                {
                    shortDist = playerXY.transform.position.y - hitXY2.transform.position.y;
                    shortPlat = hitXY2.transform;
                }
            }
            if (Physics.Raycast(rayD3, out RaycastHit hitXY3, maxDistance))
            {
                if (playerXY.transform.position.y - hitXY3.transform.position.y < shortDist)
                {
                    shortDist = playerXY.transform.position.y - hitXY3.transform.position.y;
                    shortPlat = hitXY3.transform;
                }
            }
            if (Physics.Raycast(rayD4, out RaycastHit hitXY4, maxDistance))
            {
                if (playerXY.transform.position.y - hitXY4.transform.position.y < shortDist)
                {
                    shortDist = playerXY.transform.position.y - hitXY4.transform.position.y;
                    shortPlat = hitXY4.transform;
                }
            }
        }
        if (CamDir == "YZ")
        {
            rayD1 = new Ray(playerYZ.transform.position + NE, Down);
            rayD2 = new Ray(playerYZ.transform.position + NW, Down);
            rayD3 = new Ray(playerYZ.transform.position + SE, Down);
            rayD4 = new Ray(playerYZ.transform.position + SW, Down);
            if (Physics.Raycast(rayD1, out RaycastHit hitYZ1, maxDistance))
            {
                if (playerYZ.transform.position.y - hitYZ1.transform.position.y < shortDist)
                {
                    shortDist = playerYZ.transform.position.y - hitYZ1.transform.position.y;
                    shortPlat = hitYZ1.transform;
                }
            }
            if (Physics.Raycast(rayD2, out RaycastHit hitYZ2, maxDistance))
            {
                if (playerYZ.transform.position.y - hitYZ2.transform.position.y < shortDist)
                {
                    shortDist = playerYZ.transform.position.y - hitYZ2.transform.position.y;
                    shortPlat = hitYZ2.transform;
                }
            }
            if (Physics.Raycast(rayD3, out RaycastHit hitYZ3, maxDistance))
            {
                if (playerYZ.transform.position.y - hitYZ3.transform.position.y < shortDist)
                {
                    shortDist = playerYZ.transform.position.y - hitYZ3.transform.position.y;
                    shortPlat = hitYZ3.transform;
                }
            }
            if (Physics.Raycast(rayD4, out RaycastHit hitYZ4, maxDistance))
            {
                if (playerYZ.transform.position.y - hitYZ4.transform.position.y < shortDist)
                {
                    shortDist = playerYZ.transform.position.y - hitYZ4.transform.position.y;
                    shortPlat = hitYZ4.transform;
                }
            }
        }
    }
}