using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Camera sceneCamera;

    public float moveSpeed;

    public Rigidbody2D rb;

    public Weapon weapon;

    private Vector2 moveDirection;

    private Vector3 mousePosition;

    public SpriteRenderer spriteRenderer;

    public Sprite newSprite;

    [SerializeField] private Sprite[] index;
    private SpriteRenderer _spriteRenderer;

    private float mouseCalc;
    private float mouseCalcY;
    private Transform playerPos;
    private Vector3 playerPosition;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        {
            

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 1;
             
            if(mousePosition.x - playerPos.position.x >= 0 && mousePosition.y - playerPos.position.y >= 0) 
            {
                
                if (_spriteRenderer.sprite == index[1])
                {
                    _spriteRenderer.flipX = false;
                }else if (_spriteRenderer.sprite == index[0])
                {
                    _spriteRenderer.sprite = index[1];
                }
                
            }
            
            else if (mousePosition.x - playerPos.position.x < 0 && mousePosition.y - playerPos.position.y > 0)
            {
                if (_spriteRenderer.sprite == index[1] )
                {
                    _spriteRenderer.flipX = true;

                }
                else if (_spriteRenderer.sprite == index[0])
                {
                    _spriteRenderer.sprite = index[1];
                }

            }
            else if (mousePosition.x - playerPos.position.x < 0 && mousePosition.y - playerPos.position.y  < 0)
            {
                if (_spriteRenderer.sprite == index[1])
                {
                    _spriteRenderer.sprite = index[0];
                    

                }
                else if (_spriteRenderer.sprite == index[0])
                {
                    _spriteRenderer.flipX = true;
                }

            }
            else if (mousePosition.x - playerPos.position.x >= 0 && mousePosition.y - playerPos.position.y <= 0)
            {
                if (_spriteRenderer.sprite == index[1])
                {
                    _spriteRenderer.sprite = index[0];


                }
                else if (_spriteRenderer.sprite == index[0])
                {
                    _spriteRenderer.flipX = false;
                }

            }

        }

    }
    void FixedUpdate()
    {
        // Phisics Calculations
        Move();

    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);


    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

        
  

    }

    //                         O7
    //private float pythagoreanTheorem(float x, float y)

}
