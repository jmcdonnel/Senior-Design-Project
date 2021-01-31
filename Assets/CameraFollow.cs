using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour   
{
    public Camera sceneCamera;
    private Transform playerPos;
    private Vector3 mousePosition;
    private double mouseCalc;


    void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        
    }



        // Update is called once per framea
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 1;
        
        transform.position = new Vector3(playerPos.position.x + (mousePosition.x/4), playerPos.position.y + (mousePosition.y / 4), transform.position.z);

   


    }


}
