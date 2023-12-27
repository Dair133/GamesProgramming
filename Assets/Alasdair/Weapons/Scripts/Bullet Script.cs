using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENEMY HIT");
        // Check if the bullet has collided with an enemy
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit!");

            // Optional: Destroy the enemy object
            Destroy(other.gameObject);

            // Destroy the bullet
            Destroy(gameObject);
        }
    }
}
