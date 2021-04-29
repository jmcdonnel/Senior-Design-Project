using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public int damage = 10;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Player":
                Debug.Log("Hit the player");
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
        }
    }
}
