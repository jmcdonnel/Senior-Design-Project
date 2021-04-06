using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera sceneCamera;

    public float moveSpeed;

    public Rigidbody2D rb;

    private Weapon weapon;

    public GameObject currWeapon;

    private Vector2 moveDirection;

    private Vector3 mousePosition;

    public SpriteRenderer spriteRenderer;

    public float movementSpeedMult;

    public float fireForceMult;

    public Sprite newSprite;

    [SerializeField] private Sprite[] index;
    private SpriteRenderer _spriteRenderer;

    private float mouseCalc;
    private float mouseCalcY;
    private Transform playerPos;
    private Vector3 playerPosition;

    public Animator animator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        animator = GetComponent<Animator>();
        movementSpeedMult = 1;
        fireForceMult = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        animator.SetFloat("Vertical", vertical);

        ProcessInputs();
        {
            
            //Checking which direction the player should be facing
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 1;
             
            //Mouse moved to first quadrant
            if(mousePosition.x - playerPos.position.x >= 0 && mousePosition.y - playerPos.position.y >= 0) 
            {
                //If player turned from second quadrant
                if (_spriteRenderer.sprite == index[1]) _spriteRenderer.flipX = false;
                //If player turned from fourth quadrant
                else if (_spriteRenderer.sprite == index[0]) _spriteRenderer.sprite = index[1];
            }
            //Mouse moved to second quadrant
            else if (mousePosition.x - playerPos.position.x < 0 && mousePosition.y - playerPos.position.y > 0)
            {
                //If player turned from first quadrant
                if (_spriteRenderer.sprite == index[1]) _spriteRenderer.flipX = true;
                //If player turned from third quadrant
                else if (_spriteRenderer.sprite == index[0]) _spriteRenderer.sprite = index[1];
            }
            //Mouse moved to third quadrant
            else if (mousePosition.x - playerPos.position.x < 0 && mousePosition.y - playerPos.position.y  < 0)
            {
                //If player turned from second quadrant
                if (_spriteRenderer.sprite == index[1]) _spriteRenderer.sprite = index[0];
                //If player turned from fourth quadrant
                else if (_spriteRenderer.sprite == index[0]) _spriteRenderer.flipX = true;
            }
            //Mouse moved to fourth quadrant
            else if (mousePosition.x - playerPos.position.x >= 0 && mousePosition.y - playerPos.position.y <= 0)
            {
                //If player turned from first quadrant
                if (_spriteRenderer.sprite == index[1]) _spriteRenderer.sprite = index[0];
                //If player turned from third quadrant
                else if (_spriteRenderer.sprite == index[0]) _spriteRenderer.flipX = false;
            }
        }
    }
    void FixedUpdate()
    {
        // Physics Calculations
        Move();

    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (PickUpController.getEquipped() == true && Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);


    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed * movementSpeedMult, moveDirection.y * moveSpeed * movementSpeedMult);

        
    }

    public void setCurrentWeapon()
    {
        if (GameObject.FindWithTag("Equipped Ranged Weapon") != null)
        {
            currWeapon = GameObject.FindGameObjectWithTag("Equipped Ranged Weapon");
            weapon = currWeapon.GetComponent<Weapon>();

        }
        else { 


            currWeapon = null;
            weapon = null;
            }

    }

    public void increasePlayerSpeed(float n)
    {
        movementSpeedMult += n;
    }
    public void increaseProjectileSpeed(float n)
    {
        fireForceMult += n;
    }

    public float getFireForceMult()
    {
        return fireForceMult;
    }
    //                         O7
    //private float pythagoreanTheorem(float x, float y)

}
