using UnityEngine;
using System.Collections;

public class EnemyMovementRanged : MonoBehaviour
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
    public float timeBetweenAttacks = 2f;
    public float timeBetweenMoves = 3f;
    public int attackDamage = 10;

    public Rigidbody2D projectile;
    public float projImpulse = 8.0f;
    float timer;
    float moveTimer;

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
        // Timer between moves, so enemy isn't always running
        moveTimer += Time.deltaTime;
        // Timer between attacks
        timer += Time.deltaTime;
        if (moveTimer > timeBetweenMoves && Vector2.Distance(player.position, transform.position) <= 3)
        {
            canAttack = false;
            RunAway();
        }
        else
        {
            // Make sure the enemy is standing still before attacking
            if (agent.velocity.magnitude < .05f)
            {
                canAttack = true;
            }
            if (timer >= timeBetweenAttacks && canAttack)
            {
                Attack();
            }
        }
    }

    public void RunAway()
    {
        Vector3 newPos = transform.position - (player.transform.position.normalized * 7);
        if(enemyHealth.currentHealth > 0) {
            agent.enabled = true;
            agent.SetDestination(newPos);
            moveTimer = 0f;
        }
    }
    // Attack function
    // If player is alive instantiate the enemy projectile and fire at the player
    public void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0 && enemyHealth.currentHealth > 0)
        {
            Physics2D.IgnoreLayerCollision(3,3);
            Rigidbody2D proj = (Rigidbody2D)Instantiate(projectile, transform.position, transform.rotation);
            proj.tag = "EnemyProjectile";
            // proj.GetComponent<Rigidbody2D>().AddForce(player.transform.position.normalized * projImpulse, ForceMode2D.Impulse);
            Vector2 direction = (player.transform.position - transform.position);
            proj.AddForce(direction.normalized * projImpulse, ForceMode2D.Impulse);
            Debug.Log(direction + "      " + direction.normalized);

            Destroy(proj.gameObject, 2);

            // playerHealth.TakeDamage (attackDamage);
        }
    }

}
