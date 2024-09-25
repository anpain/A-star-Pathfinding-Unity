using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float speed = 10;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (cam.orthographic && cam.orthographicSize >= 1 && cam.orthographicSize <= 60)
        {
            cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * speed;
        }
        else if(cam.orthographicSize <= 3)
        {
            cam.orthographicSize = 3;
        }
        else if (cam.orthographicSize >= 60)
        {
            cam.orthographicSize = 60;
        }
    }
}
