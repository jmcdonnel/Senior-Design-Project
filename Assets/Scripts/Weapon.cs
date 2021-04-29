using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject Projectile;

    public Transform firePoint;

    private Camera sceneCamera ;

    private Transform playerPos;

    public GameObject playerLoc;

    private PlayerController _playerController;

    public float fireForce;

    public Rigidbody2D rb;

    private Vector2 mousePosition;

    public float armLength = 1f;
    void Start()
    {
        // if the sword is child object, this is the transform of the character (or shoulder)
        playerPos = transform.parent.transform;
        sceneCamera = Camera.main;
        playerLoc = GameObject.FindGameObjectWithTag("Player"); ;
        _playerController = playerLoc.GetComponent<PlayerController>();

    }
    private void FixedUpdate()
    {
        Rotate();
    }
    private void Update()
    {
        // Get the direction between the shoulder and mouse (aka the target position)
        Vector3 playerToMouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPos.position;
        playerToMouseDir.z = 0; // zero z axis since we are using 2d
                                // we normalize the new direction so you can make it the arm's length
                                // then we add it to the shoulder's position
        transform.position = playerPos.position + (armLength * playerToMouseDir.normalized);
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

    }
    public void Fire()
    {
        GameObject projectile = Instantiate(Projectile, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * (fireForce + _playerController.getFireForceMult()), ForceMode2D.Impulse);

    }

    void Rotate()
    {
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }


}
