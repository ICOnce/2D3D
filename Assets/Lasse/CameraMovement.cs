using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform PlayerXY, PlayerXZ, PlayerYZ, CamXY, CamYZ, CamXZ;
    public static string ActiveCam;
    private int HeightDiff = 4;
    private int SideDiff = 8;
    private int DeathHeight = -3;
    void Update()
    {
        float PlayerXYx = PlayerXY.position.x;
        float PlayerXYy = PlayerXY.position.y;
        float PlayerXZx = PlayerXZ.position.x;
        float PlayerXZz = PlayerXZ.position.z;
        float PlayerYZy = PlayerYZ.position.y;
        float PlayerYZz = PlayerYZ.position.z;
        if (ActiveCam == "XY")
        {
            float CamX = CamXY.position.x;
            float CamY = CamXY.position.y;
            float CamZ = CamXY.position.z;
            if (PlayerXYx-CamX > SideDiff)
            {
                CamXY.position = new Vector3(PlayerXYx-SideDiff, CamY, CamZ);
            }
            else if (PlayerXYx - CamX < -SideDiff)
            {
                CamXY.position = new Vector3(PlayerXYx + SideDiff, CamY, CamZ);
            }
            if (PlayerXYy - CamY > HeightDiff)
            {
                CamXY.position = new Vector3(CamX, PlayerXYy - HeightDiff, CamZ);
            }
            else if (PlayerXYy - CamY < -HeightDiff && !(CamY < -3))
            {
                CamXY.position = new Vector3(CamX, PlayerXYy + HeightDiff, CamZ);
            }
        }
        if (ActiveCam == "YZ")
        {
            float CamX = CamYZ.position.x;
            float CamY = CamYZ.position.y;
            float CamZ = CamYZ.position.z;
            if (PlayerYZz-CamZ > SideDiff)
            {
                CamYZ.position = new Vector3(CamX, CamY, PlayerYZz - SideDiff);
            }
            else if (PlayerYZz-CamZ < -SideDiff)
            {
                CamYZ.position = new Vector3(CamX, CamY, PlayerYZz + SideDiff);
            }
            if (PlayerYZy - CamYZ.position.y > HeightDiff)
            {
                CamYZ.position = new Vector3(CamX, PlayerYZy-HeightDiff, CamZ);
            }
            else if (PlayerYZy - CamYZ.position.y < -HeightDiff && !(CamY < -3))
            {
                CamYZ.position = new Vector3(CamX, PlayerYZy+HeightDiff, CamZ);
            }
        }
        if (ActiveCam == "XZ")
        {
            float CamX = CamXZ.position.x;
            float CamY = CamXZ.position.y;
            float CamZ = CamXZ.position.z;
            if (PlayerXZx-CamX > SideDiff)
            {
                CamXZ.position = new Vector3(PlayerXZx - SideDiff, CamY, CamZ);
            }
            else if (PlayerXZx-CamX < -SideDiff)
            {
                CamXZ.position = new Vector3(PlayerXZx + SideDiff, CamY, CamZ);
            }
            if (PlayerXZz-CamZ > SideDiff)
            {
                CamXZ.position = new Vector3(CamX, CamY, PlayerXZz - SideDiff);
            }
            else if (PlayerXZz - CamZ < -SideDiff)
            {
                CamXZ.position = new Vector3(CamX, CamY, PlayerXZz +  SideDiff);
            }
        }
    }
}
