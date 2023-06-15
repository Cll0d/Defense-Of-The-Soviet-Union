using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camers : MonoBehaviour
{
    private Camera _mainCamera;
    public Camera MinorCamera;

    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _mainCamera = Camera.main;
    }


    public void Switch()
    {
        MinorCamera.enabled = !MinorCamera.enabled;
    }
}
    
