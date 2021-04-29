using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LockerController : MonoBehaviour
{
    private Transform player;
    private GameObject playerLoc;
    private PlayerController _playerController;
    public float pickUpRange;

    public List<GameObject> weapList;
    public List<GameObject> powerupList;

    public GameObject[] weapListArray;
    public GameObject[] powerupListArray;

    public Transform weapSpawnPoint;
    public Transform powerupSpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player"); ;
        player = playerLoc.transform;
        _playerController = playerLoc.GetComponent<PlayerController>();

        weapListArray = Resources.LoadAll<GameObject>("Weapons");
        powerupListArray = Resources.LoadAll<GameObject>("Powerups");

        weapList = weapListArray.ToList();
        powerupList = powerupListArray.ToList();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenLocker();
        }
    }

    private void OpenLocker()
    {

        GameObject weapToSpawn = weapList[Random.Range(0, weapList.Count)];
        GameObject powerupToSpawn = powerupList[Random.Range(0, powerupList.Count)];

        GameObject newWeap = Instantiate(weapToSpawn, weapSpawnPoint.position, Quaternion.identity) as GameObject;
        GameObject newPowerup = Instantiate(powerupToSpawn, powerupSpawnPoint.position, Quaternion.identity) as GameObject;

        this.enabled = false;
    }
}
