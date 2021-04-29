using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGenerator : MonoBehaviour
{

    public GameObject[] doors;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("despawnDoors", 0.8f);
    }

    void despawnDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < doors.Length; i++)
            {
                doors[i].SetActive(true);
            }
            Invoke("despawnDoors", 5.0f);
        }
    }
}
