using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public Weapon weaponScript;
    public Rigidbody2D rb;
    public Transform offHand;
    private Transform player;
    public GameObject playerLoc;
    private PlayerController _playerController;
    public GameObject currEquippedWeapon;
    private PickUpController weapon;
    private static float timeStamp;


    public float pickUpRange;



    private bool equipped;
    public static bool slotFull;

    private void Start()
    {
        offHand = GameObject.FindGameObjectWithTag("Off Hand").transform;
        playerLoc = GameObject.FindGameObjectWithTag("Player"); ;
        player = playerLoc.transform;
        _playerController = playerLoc.GetComponent<PlayerController>();
        if (!equipped)
        {
            weaponScript.enabled = false;
        }
        if (equipped)
        {
            weaponScript.enabled = true;

            slotFull = true;
        }
    }

    private void Update()
    {
        
        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
            equipped = true;
            slotFull = true;
        }
        else if (!equipped && timeStamp <= Time.time && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyUp(KeyCode.E) && slotFull)
        {
            Swap();
            equipped = true;
            slotFull = true;
        }
        //Debug.Log(gameObject + "   " +  equipped);

    }

    private void PickUp()
    {

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(offHand);
        transform.localPosition = Vector2.zero;
        transform.localRotation = Quaternion.Euler(Vector2.zero);
        transform.localScale = Vector2.one;


        //Enable script
        gameObject.tag = "Equipped Ranged Weapon";
        weaponScript.enabled = true;

        _playerController.setCurrentWeapon();
    }
    private void Drop()
    {
        equipped = false;

        //Set parent to null
        transform.SetParent(null);



        gameObject.tag = "Weapon on ground";


        //Disable script
        weaponScript.enabled = false;
 
    }
    private void Swap() {
        timeStamp = Time.time + 1;

        currEquippedWeapon = GameObject.FindWithTag("Equipped Ranged Weapon");
        weapon = currEquippedWeapon.GetComponent<PickUpController>();

        weapon.Drop();
        PickUp();

    }

    public static bool getEquipped() {
        return slotFull;
    }
}

