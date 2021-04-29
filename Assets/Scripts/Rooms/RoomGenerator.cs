using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    //public int continuedDirection = 0;
    // 0 --> none
    // 1 --> open on top
    // 2 --> open on bottom
    // 3 --> open on left
    // 4 --> open on right

    private RoomTemplates templates;
    private int rand;
    private bool started = false;
    private static bool itemRoomSpawned = false;
    //private GameObject instantiatedRoom;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    void Spawn()
    {
        if (started == false)
        {
            if (openingDirection == 1) //spawn room with bottom door
            {
                rand = UnityEngine.Random.Range(0, templates.bottomRooms.Length);

                if (itemRoomSpawned == false)
                {
                    //instantiatedRoom = (GameObject) Instantiate(templates.itemRooms[0], transform.position, templates.itemRooms[0].transform.rotation);
                    Instantiate(templates.itemRooms[0], transform.position, templates.itemRooms[0].transform.rotation);
                    //continuedDirection = 0;
                    itemRoomSpawned = true;
                }
                else
                {
                    Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);
                    /*
                    switch (rand)
                    {
                        case 0:
                            continuedDirection = 0;
                            break;
                        case 1:
                            continuedDirection = 4;
                            break;
                        case 2:
                            continuedDirection = 3;
                            break;
                        case 3:
                            continuedDirection = 1;
                            break;
                    }
                    */
                }

            }
            else if (openingDirection == 2) //spawn room with top door
            {
                rand = UnityEngine.Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);
                /*
                switch (rand)
                {
                    case 0:
                        continuedDirection = 0;
                        break;
                    case 1:
                        continuedDirection = 3;
                        break;
                    case 2:
                        continuedDirection = 4;
                        break;
                    case 3:
                        continuedDirection = 2;
                        break;
                }
                */
            }
            else if (openingDirection == 3) //spawn room with left door
            {
                rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);
                /*
                switch (rand)
                {
                    case 0:
                        continuedDirection = 0;
                        break;
                    case 1:
                        continuedDirection = 2;
                        break;
                    case 2:
                        continuedDirection = 1;
                        break;
                    case 3:
                        continuedDirection = 4;
                        break;
                }
                */
            }
            else if (openingDirection == 4) //spawn room with right door
            {
                rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);
                /*
                switch (rand)
                {
                    case 0:
                        continuedDirection = 0;
                        break;
                    case 1:
                        continuedDirection = 2;
                        break;
                    case 2:
                        continuedDirection = 1;
                        break;
                    case 3:
                        continuedDirection = 3;
                        break;
                }
                */
            }
            started = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            if(other.GetComponent<RoomGenerator>().started == false && started == false) //spawn walls blocking off any openings
            {
                //Destroy(gameObject);
                if (other.GetComponent<RoomGenerator>().openingDirection == 1 && openingDirection == 4) //need left bottom room
                {
                    try
                    {
                        Instantiate(templates.leftRooms[1], transform.position, templates.leftRooms[1].transform.rotation);
                    }
                    catch (NullReferenceException e)
                    {
                    }
                }
                if (other.GetComponent<RoomGenerator>().openingDirection == 1 && openingDirection == 3) //need right top room
                {
                    try
                    {
                        Instantiate(templates.rightRooms[2], transform.position, templates.rightRooms[2].transform.rotation);
                    }
                    catch (NullReferenceException e)
                    {
                    }
                }
                if (other.GetComponent<RoomGenerator>().openingDirection == 2 && openingDirection == 4) //need left top room
                {
                    try
                    {
                        Instantiate(templates.leftRooms[2], transform.position, templates.leftRooms[2].transform.rotation);
                    }
                    catch (NullReferenceException e)
                    {
                    }
                }
                if (other.GetComponent<RoomGenerator>().openingDirection == 2 && openingDirection == 3) //need right bottom room
                {
                    try
                    {
                        Instantiate(templates.rightRooms[1], transform.position, templates.rightRooms[1].transform.rotation);
                    }
                    catch (NullReferenceException e)
                    {
                    }
                }
            }
            /*
            else if (other.GetComponent<RoomGenerator>().started == true && started == false) // a collision has happened with a preexisting room, need to make sure parent room doesn't open to a wall
            {
                if (other.GetComponent<RoomGenerator>().continuedDirection == 3 && openingDirection == 4)
                {
                    if (continuedDirection == 1)
                    {

                    }
                    Destroy(instantiatedRoom);
                    instantiatedRoom = (GameObject) Instantiate(templates.rightRooms[2], transform.position, templates.rightRooms[2].transform.rotation);
                    continuedDirection = 4;
                }
                else if (other.GetComponent<RoomGenerator>().continuedDirection == 1 && openingDirection == 2)
                {
                    Destroy(instantiatedRoom);
                    instantiatedRoom = (GameObject)Instantiate(templates.rightRooms[2], transform.position, templates.rightRooms[2].transform.rotation);
                    continuedDirection = 4;
                }
            }
            */
            started = true;
        }
    }



}
