using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossshair : MonoBehaviour
{

    Transform _zeroPlane;


    private void Start() {
        _zeroPlane = GameObject.FindGameObjectWithTag("ZeroPlane").transform;
    }

    void Update()
    {
        PlaceCrosshair();

        transform.position = GetWorldPositionOnPlane(Input.mousePosition, 0f);
    }

    void PlaceCrosshair(){
        
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("Mouse pos: " + mousePos + "Input mouse pos: " + Input.mousePosition);
        transform.position = mousePos + Vector3.forward * 20f;
    }

    Vector3 GetWorldPositionOnPlane(Vector3 screenPos, float z){
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }
}
