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
    private float mouseCalcX;
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

        //Checking X values of mouse
        //If difference between is bigger than allowed X offset in either direction
        if (mousePosition.x - playerPos.position.x < -offsetX) mouseCalcX = -offsetX;
        else if(mousePosition.x  - playerPos.position.x > offsetX) mouseCalcX = offsetX;
        //If difference is smaller
        else mouseCalcX = mousePosition.x - playerPos.position.x;

        //Checking Y values of mouse
        //If difference between is bigger than allowed Y offset in either direction
        if (mousePosition.y - playerPos.position.y < -offsetY) mouseCalcY = -offsetY;
        else if (mousePosition.y  - playerPos.position.y > offsetY) mouseCalcY = offsetY;
        //If difference is smaller
        else mouseCalcY = mousePosition.y - playerPos.position.y;

        //mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        //Using mouseCalc values to position the camera
        transform.position = new Vector3(playerPos.position.x + mouseCalcX/4, playerPos.position.y + mouseCalcY/4, transform.position.z);

        //Debug.Log(mouseCalcX);
        //Debug.Log(mouseCalcY);


    }


}
