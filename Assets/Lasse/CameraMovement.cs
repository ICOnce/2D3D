using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform PlayerXY, PlayerXZ, PlayerYZ, CamXY, CamYZ, CamXZ;
    public static string ActiveCam;
    private int HeightDiff = 4;
    private int SideDiff = 8;
    void Update()
    {
        float PlayerXYx = PlayerXY.position.x;
        float PlayerXYy = PlayerXY.position.y;
        float PlayerXZx = PlayerXZ.position.x;
        float PlayerXZz = PlayerXZ.position.z;
        float PlayerYZy = PlayerYZ.position.y;
        float PlayerYZz = PlayerYZ.position.z;

        float CamXYX = CamXY.position.x;
        float CamXYY = CamXY.position.y;
        float CamXYZ = CamXY.position.z;

        if (PlayerXYx - CamXYX > SideDiff)
        {
            CamXY.position = new Vector3(PlayerXYx - SideDiff, CamXYY, CamXYZ);
        }
        else if (PlayerXYx - CamXYX < -SideDiff)
        {
            CamXY.position = new Vector3(PlayerXYx + SideDiff, CamXYY, CamXYZ);
        }
        if (PlayerXYy - CamXYY > HeightDiff)
        {
            CamXY.position = new Vector3(CamXYX, PlayerXYy - HeightDiff, CamXYZ);
        }
        else if (PlayerXYy - CamXYY < -HeightDiff && !(CamXYY < -3))
        {
            CamXY.position = new Vector3(CamXYX, PlayerXYy + HeightDiff, CamXYZ);
        }
        float CamYZX = CamYZ.position.x;
        float CamYZY = CamYZ.position.y;
        float CamYZZ = CamYZ.position.z;
        if (PlayerYZz - CamYZZ > SideDiff)
        {
            CamYZ.position = new Vector3(CamYZX, CamYZY, PlayerYZz - SideDiff);
        }
        else if (PlayerYZz - CamYZZ < -SideDiff)
        {
            CamYZ.position = new Vector3(CamYZX, CamYZY, PlayerYZz + SideDiff);
        }
        if (PlayerYZy - CamYZ.position.y > HeightDiff)
        {
            CamYZ.position = new Vector3(CamYZX, PlayerYZy - HeightDiff, CamYZZ);
        }
        else if (PlayerYZy - CamYZ.position.y < -HeightDiff && !(CamYZY < -3))
        {
            CamYZ.position = new Vector3(CamYZX, PlayerYZy + HeightDiff, CamYZZ);
        }

        float CamXZX = CamXZ.position.x;
        float CamXZY = CamXZ.position.y;
        float CamXZZ = CamXZ.position.z;
        if (PlayerXZx - CamXZX > SideDiff)
        {
            CamXZ.position = new Vector3(PlayerXZx - SideDiff, CamXZY, CamXZZ);
        }
        else if (PlayerXZx - CamXZX < -SideDiff)
        {
            CamXZ.position = new Vector3(PlayerXZx + SideDiff, CamXZY, CamXZZ);
        }
        if (PlayerXZz - CamXZZ > SideDiff)
        {
            CamXZ.position = new Vector3(CamXZX, CamXZY, PlayerXZz - SideDiff);
        }
        else if (PlayerXZz - CamXZZ < -SideDiff)
        {
            CamXZ.position = new Vector3(CamXZX, CamXZY, PlayerXZz + SideDiff);
        }
    }
}