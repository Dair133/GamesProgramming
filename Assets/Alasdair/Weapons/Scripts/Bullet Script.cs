using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private FollowEnemy goblin;
    private ShootRetreat storm;
    public float damage;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENEMY HIT");
        // Check if the bullet has collided with an enemy
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit!");
            goblin = other.GetComponent<FollowEnemy>();
            if (goblin == null)
            {
                storm = other.GetComponent<ShootRetreat>();
                storm.TakeDamage(damage);
            }
            else
            {
                goblin.TakeDamage(damage);
            }
            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
