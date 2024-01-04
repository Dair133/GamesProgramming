using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private FollowEnemy followEnemy;
    private ShootRetreat shootRetreatEnemy;
    private FinalBoss finalBoss;
    public float damage;

    ArrayList shootRetreatEnemyTags = new ArrayList();
    ArrayList followEnemyTags = new ArrayList();

    private void Start()
    {
        shootRetreatEnemyTags.Add("StormGhost");
        shootRetreatEnemyTags.Add("FireGhost");
       

        followEnemyTags.Add("FireBunny");
        followEnemyTags.Add("Goblin");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("ENEMY HIT");
        Debug.Log(other.tag);

        // Check if the collided object has one of the ShootRetreatEnemy tags
        foreach (string tag in shootRetreatEnemyTags)
        {
            if (other.tag == tag)
            {
                Debug.Log(tag + " hit!");

                shootRetreatEnemy = other.GetComponent<ShootRetreat>();
                if (shootRetreatEnemy != null)
                {
                    shootRetreatEnemy.TakeDamage(damage);
                }

                // Destroy the bullet
                Destroy(gameObject);

                // Exit the loop as the relevant action has been taken
                return;
            }
        }

        // Check if the collided object has one of the FollowEnemy tags
        foreach (string tag in followEnemyTags)
        {
            if (other.tag == tag)
            {
                Debug.Log(tag + " hit!");

                followEnemy = other.GetComponent<FollowEnemy>();
                if (followEnemy != null)
                {
                    followEnemy.TakeDamage(damage);
                }

                // Destroy the bullet
                Destroy(gameObject);

                // Exit the loop as the relevant action has been taken
                return;
            }
        }
        if(other.tag == "FinalBoss")
        {
            finalBoss = other.GetComponent<FinalBoss>();
            if(finalBoss != null)
            {
                finalBoss.TakeDamage(damage);
            }

            Destroy(gameObject);


            return;
        }
    }
}
