using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoomGenerator : MonoBehaviour
{
    public int openingDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    private RoomTemplates templates;
    private int rand;
    private bool started = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.8f);
    }

    void Spawn()
    {
        if (started == false)
        {
            if (openingDirection == 1) //spawn room with bottom door
            {
                rand = UnityEngine.Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);

            }
            else if (openingDirection == 2) //spawn room with top door
            {
                rand = UnityEngine.Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);

            }
            else if (openingDirection == 3) //spawn room with left door
            {
                rand = UnityEngine.Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);

            }
            else if (openingDirection == 4) //spawn room with right door
            {
                rand = UnityEngine.Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);

            }
            started = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("SpawnPoint"))
        {
            Destroy(gameObject);
        }
    }



}
