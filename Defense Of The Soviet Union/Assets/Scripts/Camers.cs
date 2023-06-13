using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camers : MonoBehaviour
{
    private Camera MainCamera;
    public Camera MinorCamera;

    void Start()
    {
        MainCamera = GetComponent<Camera>();
        MainCamera = Camera.main;
    }


    public void Switch()
    {
        MinorCamera.enabled = !MinorCamera.enabled;
    }
}
    
