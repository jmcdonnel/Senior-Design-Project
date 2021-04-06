using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpPickUpController : MonoBehaviour
{

    private Transform player;
    private GameObject playerLoc;
    private PlayerController _playerController;
    public PopUpSystem _popUpSystem;
    public float pickUpRange;

    // Start is called before the first frame update
    void Start()
    {
        playerLoc = GameObject.FindGameObjectWithTag("Player"); ;
        player = playerLoc.transform;
        _playerController = playerLoc.GetComponent<PlayerController>();
        _popUpSystem = playerLoc.GetComponent<PopUpSystem>();


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        switch (gameObject.name)
        {
            case "Speed Powerup":
                _playerController.increasePlayerSpeed(.2f);
                Destroy(gameObject);
                _popUpSystem.PopUp("Sports drink equipped - Increased movement speed by 20%");
                break;
            case "Fastball Powerup":
                _playerController.increaseProjectileSpeed(10);
                Destroy(gameObject);
                _popUpSystem.PopUp("Fastball equipped - Increased projectile speed");
                break;

        }
    }

}
