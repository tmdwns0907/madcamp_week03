using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWall : MonoBehaviour
{
    public GameObject cube;
    Vector3 MousePosition;
    Camera Camera;
    const float shootDelay = 1f;
    float shootTimer = 0;

    private void Start()
    {
        Camera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void Update(){

        if(Input.GetMouseButtonDown(0)&&(shootTimer > shootDelay))
        {
            MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            MousePosition.y=0; 

            GameObject instance = Instantiate(cube, MousePosition, Quaternion.identity) as GameObject;
            shootTimer = 0;
            Destroy(instance, 3.0f);
        }
        shootTimer += Time.deltaTime;
    }
}
