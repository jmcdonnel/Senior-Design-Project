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
    BoxCollider2D bc;
    public bool canAttack;
    public float EnemyDistance = 4.0f;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    public Rigidbody2D projectile;
    public float projImpulse = 10.0f;
    float timer;
    float moveTimer;

    void Start() {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerObject = GameObject.FindGameObjectWithTag ("Player");
        agent = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        bc = agent.GetComponent<BoxCollider2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();

    }

    void OnTriggerEnter2D(Collider2D other) {
        agent.enabled = true;
        if(other.gameObject.tag == "Player") {
        // Debug.Log(transform.position - player.transform.position + "   trans-player");
        // Debug.Log(player.transform.position - transform.position  + "   player-trans");

        // Vector3 transPlayer = transform.position - player.transform.position;
        // if(transPlayer.x > 0) {
        //     Debug.Log("X > 0");
        // }
        // else {
        //     Debug.Log("X < 0");
        // }
        // if(transPlayer.y > 0) {
        //     Debug.Log("Y > 0");
        // }
        // else {
        //     Debug.Log("Y < 0");
        // }
        Vector3 newPos = transform.position - (player.transform.position.normalized * 7);
        agent.SetDestination(newPos);
        canAttack = false;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        agent.enabled = true;
        moveTimer += Time.deltaTime;
        if(other.gameObject.tag == "Player" && moveTimer > 2.0f) {
        Vector3 sqrDistance = player.transform.position + transform.position;
        // sqrDistance *= 2;
        Vector3 newPos = transform.position - (player.transform.position.normalized * 5);
        agent.SetDestination(newPos);
        canAttack = false;
        moveTimer = 0f;
        }
    }

    void OnTriggerExit2D() {
        // agent.enabled = false;
        bc.isTrigger = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            bc.isTrigger = true;
        }
        else if(other.gameObject.tag == "Wall") {
            agent.enabled = false;
        }
    }
    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            bc.isTrigger = false;
        }
    }

    void Update ()
    {
        if(agent.velocity.magnitude < .15f) {
            canAttack = true;
        }
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && enemyHealth.currentHealth > 0 && canAttack)
        {
            Attack ();
        }

    }

    public void Attack ()
    {
       timer = 0f;
       if(playerHealth.currentHealth > 0)
       {
            Rigidbody2D proj = (Rigidbody2D) Instantiate(projectile, transform.position, transform.rotation);
            proj.GetComponent<Rigidbody2D>().AddForce(player.transform.position.normalized * projImpulse, ForceMode2D.Impulse); //.GetComponent<Rigidbody2D>()

            Destroy(proj.gameObject, 2);
        //    playerHealth.TakeDamage (attackDamage);
       }
    }
    
}
