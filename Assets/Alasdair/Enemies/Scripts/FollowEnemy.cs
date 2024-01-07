using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    private PlayerScripts playerHealthScript;
    private GameObject player;
    private float lastAttackTime = 0f;
    public float speed;
    public Transform target;
    public float AggroRange;

    public GameObject deathSound;
    private Vector2 steeringForce;
    public float stopRange = 2f;//how close to the player the enemy should stop moving(prevents enemy literally walking on top of player)
    public Animator enemyAnimator;  // Animator component
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    //Health Variables



    public float health;
    public float damage = 1;
    [SerializeField] FloatingHealthBar healthBar;
    
    private void Start()
    {
        //Health Variables
        
        healthBar = GetComponentInChildren<FloatingHealthBar>();
   
        if (enemyAnimator == null)
        {
            enemyAnimator = GetComponent<Animator>();
        }
        if(damage == -1)
        {
            damage = 2;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    void Update()
    {
        FollowAttackTarget();
    }
    void FollowAttackTarget()
    {
        steeringForce = Vector2.zero;

        if (target == null)
        {
            player = GameObject.FindGameObjectWithTag("PlayerTag");
            if (player != null)
            {
                target = player.GetComponent<Transform>();
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            // Sense the environment by casting rays
            for (float angle = -30f; angle <= 30f; angle += 10f)
            {
                Vector2 rayDirection = Quaternion.Euler(0, 0, angle) * (target.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 1.0f);

                if (hit.collider != null && hit.collider.tag == "Wall")
                {
                    // Debug.Log("Wall detected!");
                    // Steering away from the wall by reversing the ray direction
                    steeringForce -= rayDirection * 0.5f;
                }
            }

            // Add the steering force to the desired direction
            Vector2 desiredDirection = ((Vector2)target.position - (Vector2)transform.position).normalized;
            desiredDirection += steeringForce;
            desiredDirection.Normalize();

            // Move toward the player while avoiding the walls
            if (distanceToPlayer < AggroRange && distanceToPlayer > 1.0f && distanceToPlayer > stopRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + desiredDirection, speed * Time.deltaTime);
                enemyAnimator.SetBool("isMoving", true);
                enemyAnimator.SetBool("isAttacking", false);
            }
            else if(distanceToPlayer < AggroRange)
            {
                // if (Time.time - lastAttackTime >= 0.1f)
                // {
                StartCoroutine(damagePlayer(damage));
                //}
                enemyAnimator.SetBool("isMoving", false);
                enemyAnimator.SetBool("isAttacking", true);
            }

        }



    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(FlashRed());
        health -= dmg;
        healthBar.UpdateHealthBar(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject deathSoundRef = GameObject.Instantiate(deathSound);
            Destroy(deathSoundRef, 5f);
            GameObject coin = Instantiate(Resources.Load<GameObject>("Coin"), transform.position, Quaternion.identity);
            //Gold worth from Goblins
            coin.GetComponent<CoinPickup>().value = 5;
        }
    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
    IEnumerator damagePlayer(float dmg)
    {
        dealDamageToPlayer(dmg);
        yield return null;
    }

    void dealDamageToPlayer(float dmg)
    {
       
        playerHealthScript = player.GetComponent<PlayerScripts>();
        playerHealthScript.TakeDamage(dmg);
    }
}
