using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraChange : MonoBehaviour
{
    //Public variables
    public static string CamDir;
    public Transform playerYZ, playerXZ, playerXY, currentPlayer;

    //Private variables
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
        //Set position of non-current players
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
        
        //save current direction of camera
        PrevDir = CamDir;

        //Attempt to change camera to XY
        if (Input.GetKeyDown("1") && CamDir != "camXY" && currentPlayer.GetComponent<Movement>().onGround == true)
        {
            //Check if the move is legal, if not stop here
            if (CheckForCollider(playerXY.position, "XY") == false) return;

            //Change active camera
            camXY.enabled = true;
            camXZ.enabled = false;
            camYZ.enabled = false;

            //Remember the active camera & player
            CamDir ="camXY";
            currentPlayer = playerXY;
            GetComponent<UIHandler>().SetCam(camXY);
            CameraMovement.ActiveCam = "XY";

            //disable/enable movementscrips & gravity
            playerXY.GetComponent<Movement>().enabled = true;
            playerXZ.GetComponent<Movement>().enabled = false;
            playerYZ.GetComponent<Movement>().enabled = false;
            playerXY.GetComponent<Rigidbody>().useGravity = true;
            playerXZ.GetComponent<Rigidbody>().useGravity = false;
            playerYZ.GetComponent<Rigidbody>().useGravity = false;

            //Figure out the height of the player if the previous was the XZ plane
            if (PrevDir == "camXZ")
            {
                GetTheHeight();
            }
        }
        //Attempt to change camera to XZ (works similar to XY, but does less)
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
        //Attempt to change camera to YZ (works the same as XY)
        else if (Input.GetKeyDown("3") && CamDir != "camYZ" && currentPlayer.GetComponent<Movement>().onGround == true)
        {
            if (CheckForCollider(playerYZ.position, "YZ") == false) return;
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
        //First check what cameraview the player wants to change to
        if (newCamDir == "XY")
        {
            //Set the proper Rays based on the viewpoint
            rayD1 = new Ray(X + NE, Down);
            rayD2 = new Ray(X + NW, Down);
            rayD3 = new Ray(X + SE, Down);
            rayD4 = new Ray(X + SW, Down);
            rayE1 = new Ray(X + SE, East);
            rayE2 = new Ray(X + NE, East);
            rayW1 = new Ray(X + SW, West);
            rayW2 = new Ray(X + NW, West);
            //Check if the move is legal via rays
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
            //If the desired plane is XZ, set up rays to check above the old direction
            if (PrevDir == "camXY")
            {
                rayU1 = new Ray(playerXY.transform.position + NE, Up);
                rayU2 = new Ray(playerXY.transform.position + NW, Up);
                rayU3 = new Ray(playerXY.transform.position + SE, Up);
                rayU4 = new Ray(playerXY.transform.position + SW, Up);
            }
            else if (PrevDir == "camYZ")
            {
                rayU1 = new Ray(playerXZ.transform.position + NE, Up);
                rayU2 = new Ray(playerXZ.transform.position + NW, Up);
                rayU3 = new Ray(playerXZ.transform.position + SE, Up);
                rayU4 = new Ray(playerXZ.transform.position + SW, Up);
            }
            //Check if the change is legal
            if (Physics.Raycast(rayU1, maxDist) || Physics.Raycast(rayU2, maxDist) || Physics.Raycast(rayU3, maxDist) || Physics.Raycast(rayU4, maxDist)) return false;

            //set up rays from the actual new player
            rayD1 = new Ray(X + NE, Down);
            rayD2 = new Ray(X + NW, Down);
            rayD3 = new Ray(X + SE, Down);
            rayD4 = new Ray(X + SW, Down);
            rayU1 = new Ray(X + NE, Up);
            rayU2 = new Ray(X + NW, Up);
            rayU3 = new Ray(X + SE, Up);
            rayU4 = new Ray(X + SW, Up);

            //check if the change is legal
            if ((Physics.Raycast(rayD1, maxDist) || Physics.Raycast(rayD2, maxDist) || Physics.Raycast(rayD3, maxDist) || Physics.Raycast(rayD4, maxDist)) && !Physics.Raycast(rayU1, maxDist) && !Physics.Raycast(rayU2, maxDist) && !Physics.Raycast(rayU3, maxDist) && !Physics.Raycast(rayU4, maxDist))
            {
                return true;
            }
        }

        //Same as XZ change
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

        //If none of the above has returned, return false
        return false;
    }
    private void GetTheHeight()
    {
        //Setup of variables & rays
        float shortDist=500000;
        float YHeight=0;
        Vector3 Height = playerXZ.transform.position + new Vector3(500, 100, 0);
        rayD1 = new Ray(Height + NE, Down);
        rayD2 = new Ray(Height + NW, Down);
        rayD3 = new Ray(Height + SE, Down);
        rayD4 = new Ray(Height + SW, Down);
        
        //Check if any of the four rays hit anything, and save whatever is closest to the source of the ray
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
        //Set the players Y-position to be above the closest platform
        if (CamDir == "camXY") playerXY.transform.position = new Vector3(playerXY.transform.position.x, YHeight + 1.2f, playerXY.transform.position.z);
        if (CamDir == "camYZ") playerYZ.transform.position = new Vector3(playerYZ.transform.position.x, YHeight + 1.2f, playerYZ.transform.position.z);
    }
}