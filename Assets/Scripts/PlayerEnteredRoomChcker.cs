using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnteredRoomChcker : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject darkness;
    public GameObject spawner;
    public bool playerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerEnter = true;
        if (collision.tag == "Player")
        {
            darkness.SetActive(false);
            spawner.SetActive(true);
        }

    }
    
    // Update is called once per frame

}
