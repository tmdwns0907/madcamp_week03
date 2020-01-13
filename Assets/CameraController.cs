﻿
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed = 150f; 
    public float panBorderThickness = 10f;


    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(Input.GetKey("w")||Input.mousePosition.y >= Screen.height - panBorderThickness){
            pos.z += panSpeed * Time.deltaTime;
        }
        if(Input.GetKey("s")||Input.mousePosition.y <= panBorderThickness){
            pos.z -= panSpeed * Time.deltaTime;
        }
        if(Input.GetKey("d")||Input.mousePosition.x >= Screen.width - panBorderThickness){
            pos.x += panSpeed * Time.deltaTime;
        }
        if(Input.GetKey("a")||Input.mousePosition.x <= panBorderThickness){
            pos.x -= panSpeed * Time.deltaTime;
        }




        transform.position = pos;
    }
}