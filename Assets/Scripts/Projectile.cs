using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody2D rb;

    public GameObject enemy;
    public int damage = 10;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Destroy(gameObject);
                break;
            case "Enemy":
                EnemyHealth enemyHealth = collision.GetComponent <EnemyHealth> ();
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);
                break;
        }
    }
}
