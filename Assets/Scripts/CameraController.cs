using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float dragSpeed;

    private int currentZoomlevel = 2;
    private float[] zoomlevels = new float[] { 2f, 4f, 8f, 16f, 32f };

    // for map drag
    Vector3 oldMousePosition;
    bool doDrag = false;


    private void Start()
    {
        oldMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        HandleMove();
        HandleZoom();
        HandleDrag();
    }

    private void HandleMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Camera.main.transform.position += new Vector3(x, y, 0) * moveSpeed;
    }

    private void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            currentZoomlevel--;
            if (currentZoomlevel < 0) currentZoomlevel = 0;
            Camera.main.orthographicSize = zoomlevels[currentZoomlevel];
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            currentZoomlevel++;
            if (currentZoomlevel >= zoomlevels.Length) currentZoomlevel = zoomlevels.Length - 1;
            Camera.main.orthographicSize = zoomlevels[currentZoomlevel];
        }
    }

    private void HandleDrag()
    {
        if(Input.GetMouseButtonDown(2))
        {
            doDrag = true;
            oldMousePosition = Input.mousePosition;
        }
        if(Input.GetMouseButtonUp(2))
        {
            doDrag = false;
        }
        if(doDrag)
        {
            gameObject.transform.position += (oldMousePosition - Input.mousePosition) * dragSpeed;
            oldMousePosition = Input.mousePosition;
        }
    }
}