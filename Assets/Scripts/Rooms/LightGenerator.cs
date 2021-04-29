using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGenerator : MonoBehaviour
{
    public GameObject[] lights;

    void Start()
    {
        Invoke("Spawn", 0.8f);
    }

    void Spawn()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(false);
            //Instantiate(lights[i], transform.position, lights[i].transform.rotation);
        }
    }

    public void turnOnLights()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            lights[i].SetActive(true);
            //Instantiate(lights[i], transform.position, lights[i].transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < lights.Length; i++)
            {
                lights[i].SetActive(true);
            }

        }
    }


    
}
