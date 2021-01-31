using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour   
{
    public Camera sceneCamera;

    private Transform playerPos;
    private Vector3 playerPosition;
    private Vector3 mousePosition;
    private Vector3 tmp;
    private float mouseCalc;
    private float mouseCalcY;
    private float offsetX = 8.8f;
    private float offsetY = 4.4f;


    void Awake()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    }



        // Update is called once per frame
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 1;


        if(mousePosition.x - playerPos.position.x < -offsetX)
        {
            mouseCalc = -offsetX;
        }else if(mousePosition.x  - playerPos.position.x > offsetX)
        {
            mouseCalc = offsetX;
        }
        else
        {
            mouseCalc = mousePosition.x - playerPos.position.x;
        }
        if (mousePosition.y - playerPos.position.y < -offsetY)
        {
            mouseCalcY = -offsetY;
        }
        else if (mousePosition.y  - playerPos.position.y > offsetY)
        {
            mouseCalcY = offsetY;
        }
        else {
            mouseCalcY = mousePosition.y - playerPos.position.y;
        }
        
        //mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(playerPos.position.x + mouseCalc/4, playerPos.position.y + mouseCalcY/4, transform.position.z);

        Debug.Log(mouseCalc);
        //Debug.Log(mouseCalcY);


    }


}
