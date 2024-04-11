using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraChange : MonoBehaviour
{
    public static string CamDir;
    public Transform playerYZ, playerXZ, playerXY, currentPlayer;

    [SerializeField] private Camera camXY, camXZ, camYZ;
    private float maxDistance = 1000;
    private int maxDist = 250;
    private string PrevDir;

    //Setting the vectors for the rays & the rays
    private Vector3 Down = new Vector3(0, -10000, 0), West = new Vector3(0, 0, 1), East = new Vector3(0, 0, -1), Up = new Vector3(0, 1, 0), North = new Vector3(1, 0, 0), South = new Vector3(-1,0,0);
    Ray ray, rayD1, rayD2, rayD3, rayD4, rayW1, rayW2, rayE1, rayE2, rayN1, rayN2, rayS1, rayS2, rayU1, rayU2, rayU3, rayU4;
    private Vector3 NE = new Vector3(0.499f,-0.99f,0.499f), NW = new Vector3(0.499f, -0.99f, -0.499f), SE = new Vector3(-0.499f, -0.99f, 0.499f), SW = new Vector3(-0.499f, -0.99f, -0.499f);

    void Update()
    {
        Vector3 Height = playerYZ.transform.position + new Vector3(0, 100, 0);
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
        if (Input.GetKeyDown("1") && CamDir != "camXY" && currentPlayer.GetComponent<Movement>().onGround == true)
        {
            if (CheckForCollider(playerXY.position, "XY") == false) return;
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;
            CamDir ="camXY";
            currentPlayer = playerXY;
            GetComponent<UIHandler>().SetCam(camXY);
            playerXY.GetComponent<Movement>().enabled = true;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = false;
            playerXY.GetComponent<Rigidbody>().useGravity = true;
            playerXZ.GetComponent<Rigidbody>().useGravity = false;
            playerYZ.GetComponent<Rigidbody>().useGravity = false;
            CameraMovement.ActiveCam = "XY";
            if (PrevDir == "camXZ")
            {
                GetTheHeight();
            }
        }
        else if (Input.GetKeyDown("2") && CamDir != "camXZ" && currentPlayer.GetComponent<Movement>().onGround == true)
        {
            if (CheckForCollider(playerXZ.position, "XZ") == false) return;
            camXY.enabled = false;
            camXZ.enabled = true;
            camYZ.enabled = false;
            CamDir = "camXZ";
            currentPlayer = playerXZ;
            GetComponent<UIHandler>().SetCam(camXZ);
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = true;
            playerYZ.GetComponent<Movement>().enabled = false;
            playerXY.GetComponent<Rigidbody>().useGravity = false;
            playerXZ.GetComponent<Rigidbody>().useGravity = true;
            playerYZ.GetComponent<Rigidbody>().useGravity = false;
            CameraMovement.ActiveCam = "XZ";
        }
        else if (Input.GetKeyDown("3") && CamDir != "camYZ" && currentPlayer.GetComponent<Movement>().onGround == true)
        {
            if (CheckForCollider(playerYZ.position, "YZ") == false)
            {
                return;
            }
            camXY.enabled = false;
            camXZ.enabled = false;
            camYZ.enabled = true;
            CamDir = "camYZ";
            currentPlayer = playerYZ;
            GetComponent<UIHandler>().SetCam(camYZ);
            playerXY.GetComponent<Movement>().enabled = false;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = true;
            playerXY.GetComponent<Rigidbody>().useGravity = false;
            playerXZ.GetComponent<Rigidbody>().useGravity = false;
            playerYZ.GetComponent<Rigidbody>().useGravity = true;
            CameraMovement.ActiveCam = "YZ";
            if (PrevDir == "camXZ") GetTheHeight();
        }
    }
    private bool CheckForCollider(Vector3 X, string newCamDir)
    {
        if (CamDir == "camXZ")
        {
            ray = new Ray(X + new Vector3(0,200,0), Down);
            if (Physics.Raycast(ray, out RaycastHit hit, maxDist)) X = hit.point + new Vector3(0,1,0);
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
            if ((Physics.Raycast(rayD1, maxDist) || Physics.Raycast(rayD2, maxDist) || Physics.Raycast(rayD3, maxDist) || Physics.Raycast(rayD4, maxDist)) && !Physics.Raycast(rayE1, maxDist) && !Physics.Raycast(rayE2, maxDist) && !Physics.Raycast(rayW1, maxDist) && !Physics.Raycast(rayW2, maxDist))
            {               
                return true;
            }
            else if (Physics.Raycast(rayE1, maxDist) || Physics.Raycast(rayW1, maxDist))
            {
                return false;
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
            if ((Physics.Raycast(rayD1, maxDist) || Physics.Raycast(rayD2, maxDist) || Physics.Raycast(rayD3, maxDist) || Physics.Raycast(rayD4, maxDist)) && !Physics.Raycast(rayU1, maxDist) && !Physics.Raycast(rayU2, maxDist) && !Physics.Raycast(rayU3, maxDist) && !Physics.Raycast(rayU4, maxDist))
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
            if ((Physics.Raycast(rayD1, maxDist) || Physics.Raycast(rayD2, maxDist) || Physics.Raycast(rayD3, maxDist) || Physics.Raycast(rayD4, maxDist)) && !Physics.Raycast(rayN1, maxDist) && !Physics.Raycast(rayN2, maxDist) && !Physics.Raycast(rayS1, maxDist) && !Physics.Raycast(rayS2, maxDist))
            {
                return true;
            }
            else if (Physics.Raycast(rayN1, maxDist) || Physics.Raycast(rayN2, maxDist) || Physics.Raycast(rayS1, maxDist) || Physics.Raycast(rayS2, maxDist))
            {
                return false;
            }
        }
        return false;
    }
    private void GetTheHeight()
    {
        float shortDist=500000;
        float YHeight=0;
        Vector3 Height = playerXZ.transform.position + new Vector3(500, 100, 0);
        rayD1 = new Ray(Height + NE, Down);
        rayD2 = new Ray(Height + NW, Down);
        rayD3 = new Ray(Height + SE, Down);
        rayD4 = new Ray(Height + SW, Down);
        if (Physics.Raycast(rayD1, out RaycastHit hitXY1, maxDistance))
        {
            if (Height.y - hitXY1.transform.position.y < shortDist)
            {
                shortDist = Height.y - hitXY1.transform.position.y;
                YHeight = hitXY1.transform.position.y+hitXY1.transform.localScale.y/2;
            }
        }
        if (Physics.Raycast(rayD2, out RaycastHit hitXY2, maxDistance))
        {
            if (Height.y - hitXY2.transform.position.y < shortDist)
            {
                shortDist = Height.y - hitXY2.transform.position.y;
                YHeight = hitXY2.transform.position.y + hitXY2.transform.localScale.y / 2;
            }
        }
        if (Physics.Raycast(rayD3, out RaycastHit hitXY3, maxDistance))
        {
            if (Height.y - hitXY3.transform.position.y < shortDist)
            {
                shortDist = Height.y - hitXY3.transform.position.y;
                YHeight = hitXY3.transform.position.y + hitXY3.transform.localScale.y / 2;
            }
        }
        if (Physics.Raycast(rayD4, out RaycastHit hitXY4, maxDistance))
        {
            if (Height.y - hitXY4.transform.position.y < shortDist)
            {
                shortDist = Height.y - hitXY4.transform.position.y;
                YHeight = hitXY4.transform.position.y + hitXY4.transform.localScale.y / 2;
            }
        }
        if (CamDir == "camXY") playerXY.transform.position = new Vector3(playerXY.transform.position.x, YHeight + 1.2f, playerXY.transform.position.z);
        if (CamDir == "camYZ") playerYZ.transform.position = new Vector3(playerYZ.transform.position.x, YHeight + 1.2f, playerYZ.transform.position.z);
    }
}