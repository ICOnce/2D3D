using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform Player, CamXY, CamYZ, CamXZ;
    public static string ActiveCam;
    private int HeightDiff = 4;
    private int SideDiff = 8;
    void Update()
    {
        float PlayerX = Player.position.x;
        float PlayerY = Player.position.y;
        float PlayerZ = Player.position.z;
        if (ActiveCam == "XY")
        {
            float CamX = CamXY.position.x;
            float CamY = CamXY.position.y;
            float CamZ = CamXY.position.z;
            if (PlayerX-CamX > SideDiff)
            {
                CamXY.position = new Vector3(PlayerX-SideDiff, CamY, CamZ);
            }
            else if (PlayerX - CamX < -SideDiff)
            {
                CamXY.position = new Vector3(PlayerX + SideDiff, CamY, CamZ);
            }
            if (PlayerY - CamY > HeightDiff)
            {
                CamXY.position = new Vector3(CamX, PlayerY - HeightDiff, CamZ);
            }
            else if (PlayerY - CamY < -HeightDiff)
            {
                CamXY.position = new Vector3(CamX, PlayerY + HeightDiff, CamZ);
            }
        }
        if (ActiveCam == "YZ")
        {
            float CamX = CamYZ.position.x;
            float CamY = CamYZ.position.y;
            float CamZ = CamYZ.position.z;
            if (PlayerZ-CamZ > SideDiff)
            {
                CamYZ.position = new Vector3(CamX, CamY, PlayerZ - SideDiff);
            }
            else if (PlayerZ-CamZ < -SideDiff)
            {
                CamYZ.position = new Vector3(CamX, CamY, PlayerZ + SideDiff);
            }
            if (PlayerY - CamYZ.position.y > HeightDiff)
            {
                CamYZ.position = new Vector3(CamX, PlayerY-HeightDiff, CamZ);
            }
            else if (PlayerY - CamYZ.position.y < -HeightDiff)
            {
                CamYZ.position = new Vector3(CamX, PlayerY+HeightDiff, CamZ);
            }
        }
        if (ActiveCam == "XZ")
        {
            float CamX = CamXZ.position.x;
            float CamY = CamXZ.position.y;
            float CamZ = CamXZ.position.z;
            if (PlayerX-CamX > SideDiff)
            {
                CamXZ.position = new Vector3(PlayerX - SideDiff, CamY, CamZ);
            }
            else if (PlayerX-CamX < -SideDiff)
            {
                CamXZ.position = new Vector3(PlayerX + SideDiff, CamY, CamZ);
            }
            if (PlayerZ-CamZ > SideDiff)
            {
                CamXZ.position = new Vector3(CamX, CamY, PlayerZ - SideDiff);
            }
            else if (PlayerZ - CamZ < -SideDiff)
            {
                CamXZ.position = new Vector3(CamX, CamY, PlayerZ +  SideDiff);
            }
        }
    }
}
