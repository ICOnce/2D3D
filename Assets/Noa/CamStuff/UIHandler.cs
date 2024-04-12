using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject display1, display2;
    [SerializeField] private Material XY, YZ, XZ;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera startCam;
    [SerializeField] private int Level;
    // Start is called before the first frame update
    void Start()
    {
        if (startCam.name == "CamXY")
        {
            canvas.GetComponent<Canvas>().worldCamera = startCam;
            display1.GetComponent<Image>().material = XZ;
            display2.GetComponent<Image>().material = YZ;
        }
        if (startCam.name == "CamYZ")
        {
            canvas.GetComponent<Canvas>().worldCamera = startCam;
            display1.GetComponent<Image>().material = XZ;
            display2.GetComponent<Image>().material = XY;
        }
        if (startCam.name == "CamXZ")
        {
            canvas.GetComponent<Canvas>().worldCamera = startCam;
            display1.GetComponent<Image>().material = XY;
            display2.GetComponent<Image>().material = YZ;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetCam(Camera cam)
    {
        if (cam.name == "CamXY")
        {
            canvas.GetComponent<Canvas>().worldCamera = cam;
            display1.GetComponent<Image>().material = XZ;
            display2.GetComponent<Image>().material = YZ;
        }
        if (cam.name == "CamYZ")
        {
            canvas.GetComponent<Canvas>().worldCamera = cam;
            display1.GetComponent<Image>().material = XZ;
            display2.GetComponent<Image>().material = XY;
        }
        if (cam.name == "CamXZ")
        {
            canvas.GetComponent<Canvas>().worldCamera = cam;
            display1.GetComponent<Image>().material = XY;
            display2.GetComponent<Image>().material = YZ;
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Level" + (Level + 1));
    }
}
