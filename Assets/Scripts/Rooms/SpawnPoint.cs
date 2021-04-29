using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public Transform objectPos;
    public 
    //public GameObject playerCheck;
    int randEnemy;
    Vector2 spawnLoc;
    private int spawnCount;
    private int count;
    public float spawnRate = 2;
    float nextSpawn = 0;

    float randX;
    float randY;
    float minX;
    float maxX;
    float minY;
    float maxY;
    void Start()
    {
        minX = objectPos.position.x - 11.5f;
        maxX = objectPos.position.x + 10.5f;
        maxY = objectPos.position.y + 3.5f;
        minY = objectPos.position.y - 6.5f;
        count = 0;
        spawnCount = 4;
        nextSpawn = Time.time + spawnRate;
    }
    // Update is called once per frame
    void Update()
    {
        //if (playerCheck.GetComponent<SpawnPoint>().playerCheck)
        //{
        //    gameObject.SetActive(true);
            
        //}
        //if (gameObject.activeSelf)
        //{
            if ( count < spawnCount && Time.time > nextSpawn)
            {
                count++;
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(minX, maxX);
                randY = Random.Range(minY, maxY);
                randEnemy = Random.Range(0, 2);
                spawnLoc = new Vector2(randX, randY);

                Instantiate(enemies[randEnemy], spawnLoc, Quaternion.identity);
                
            }

            
        //}
    }
}
