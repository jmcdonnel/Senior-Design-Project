using UnityEngine;
using System.Collections;

public class EnemyBossMovement : MonoBehaviour
{
    Transform player;
    GameObject playerObject;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent agent;
    Vector3 moveDirection;
    public BoxCollider2D bc;
    public bool canAttack;
    public float EnemyDistance = 4.0f;
    public int attackDamage = 20;

    public Rigidbody2D projectile;
    public float projImpulse = 6.0f;
    float tripleTimer, threeSixtyTimer, meleeTimer;
    float tripleAttackSleep = 2.0f;
    float threeSixtyAttackSleep = 5.0f;
    float meleeAttackSleep = 3.0f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        bc = agent.GetComponent<BoxCollider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
    }


    // void OnTriggerStay2D(Collider2D other) {
    //     agent.enabled = true;
    //     moveTimer += Time.deltaTime;
    //     if(other.gameObject.tag == "Player" && moveTimer > 2.0f) {
    //     Vector3 sqrDistance = player.transform.position + transform.position;
    //     // sqrDistance *= 2;
    //     Vector3 newPos = transform.position - (player.transform.position.normalized * 5);
    //     agent.SetDestination(newPos);
    //     canAttack = false;
    //     moveTimer = 0f;
    //     }
    // }

    // void OnTriggerExit2D() {
    //     // agent.enabled = false;
    //     // bc.isTrigger = false;
    // }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // bc.isTrigger = true;
        }
        else if (other.gameObject.tag == "Wall")
        {
            agent.enabled = false;
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // bc.isTrigger = false;
        }
    }

    void Update()
    {
        // Timer between normal ranged attack
        tripleTimer += Time.deltaTime;
        // Timer between 360 attack
        threeSixtyTimer += Time.deltaTime;
        // Timer between melee attacks
        meleeTimer += Time.deltaTime;

        if (Vector2.Distance(player.position, transform.position) <= 2.0f)
        {
            if (meleeTimer >= meleeAttackSleep)
            {
                Attack();
            }
        }
        if (Vector2.Distance(player.position, transform.position) > 2.0f)
        {
            if (tripleTimer >= tripleAttackSleep)
            {
                TripleAttack();
            }
            if (threeSixtyTimer >= threeSixtyAttackSleep)
            {
                ThreeSixtyAttack();
            }
        }
        Chase();
    }

    public void Chase()
    {
        Debug.Log(enemyHealth.currentHealth + "      " + playerHealth.currentHealth);
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth >  0) // enemyHealth.currentHealth > 0 && 
        {
            agent.SetDestination (player.position);
        }
        else
        {
            agent.enabled = false;
        }
    }
    // Attack functions
    // Basic melee attack
    public void Attack()
    {
        meleeTimer = 0f;
        Debug.Log("Attacking");
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
    // If player is alive instantiate three enemy projectiles and fire at the player
    public void TripleAttack()
    {
        tripleTimer = 0f;
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 3);
            int numberOfShots = 3;
            float degree = 90f;
            for (float i = 0; i < numberOfShots; ++i)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, degree);

                Physics2D.IgnoreLayerCollision(3, 3);
                Rigidbody2D proj = (Rigidbody2D)Instantiate(projectile, this.transform.position, rotation);
                proj.tag = "EnemyProjectile";
                // proj.GetComponent<Rigidbody2D>().AddForce(player.transform.position.normalized * projImpulse, ForceMode2D.Impulse);
                Vector3 direction = (player.transform.position - transform.position);
                proj.AddForce(-(proj.transform.right - direction).normalized * projImpulse, ForceMode2D.Impulse);
                Debug.Log(direction + "      " + direction.normalized);
                Destroy(proj.gameObject, 5);
                degree += degree;
            }
        }
    }

    public void ThreeSixtyAttack()
    {
        threeSixtyTimer = 0f;
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            Physics2D.IgnoreLayerCollision(3, 3);
            int numberOfShots = 12;
            float degree = 360f / numberOfShots;
            for (float i = -180f; i < 180f; i += degree)
            {
                Quaternion rotation = Quaternion.Euler(0, 0, i);

                Physics2D.IgnoreLayerCollision(3, 3);
                Rigidbody2D proj = (Rigidbody2D)Instantiate(projectile, this.transform.position, rotation);
                proj.tag = "EnemyProjectile";
                // proj.GetComponent<Rigidbody2D>().AddForce(player.transform.position.normalized * projImpulse, ForceMode2D.Impulse);
                // Vector2 direction = (player.transform.position - transform.position);
                proj.AddForce(proj.transform.right * projImpulse, ForceMode2D.Impulse);
                // Debug.Log(direction + "      " + direction.normalized);

                Destroy(proj.gameObject, 5);
            }
        }
    }
}
