using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRetreat : MonoBehaviour
{
    public float retreatSpeed;//how fast enemy retreats when player gets close
    public float followSpeed;//how fast enemy follows when enemy is following
    public Transform target;

    public Animator enemyAnimator;

    public float AggroRange;//range at which player will begin attacking/moving/retreating(Enemy will never retreat out of aggro range)
    public float moveTowardRange;//how far away must player be for enemy to start moving toward
    public float retreatRange;//how close must the player be for enemy to retreat
  
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float shootInterval = 3f;//how many times enemy shoots per second
    private float nextShootTime;


    public float BulletSpawnLocationY;
    public float BulletSpawnLocationX;
    public float shootDelay;//dont edit unless know what it does can make animations weird
    public float animationDelay;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float shootingAnimationEndTime;

    //Health Variables
    private float health;
    [SerializeField]
    private FloatingHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        //Health Variables
        health = 100;
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        
        if (enemyAnimator == null)//gets animator if not set publically
        {
            enemyAnimator = GetComponent<Animator>();
        }
        followSpeed = 7;
        retreatSpeed = 8;
        nextShootTime = Time.time;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        MoveAndShoot();
    }

    void MoveAndShoot()
    {
        if (target == null)//if enemy does not have target to shoot(player not assigned in inspector) we find the player and assign target
        {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
            if (player != null)
            {
                target = player.GetComponent<Transform>();
            }
        }
        else
        {

            float distanceToPlayer = Vector2.Distance(transform.position, target.position);//calculates distance to player
           
            if (distanceToPlayer < AggroRange)
            {
                //0.8f here stops floating point imprecisions where ghost is retreating one frame and moving towards player next frame despite
                //being stationery, basically stops weird "vibrating in place" from happening
                if (distanceToPlayer < retreatRange-0.8f)
                {
                    enemyAnimator.SetBool("isMoving", true);
                    transform.position = Vector2.MoveTowards(transform.position, target.position, -retreatSpeed * Time.deltaTime);
                }
                else if (distanceToPlayer >= moveTowardRange)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
                    enemyAnimator.SetBool("isMoving", true);
                }

                if (Time.time >= nextShootTime)
                {
                    StartCoroutine(PlayShootingAnimation(animationDelay,true));
                    Vector2 rayStart = transform.position + (target.position - transform.position).normalized * -0.5f;
                    RaycastHit2D hit = Physics2D.Raycast(rayStart, target.position - transform.position, AggroRange);

                    if (hit.collider != null)//if enemy finds something to shoot at
                    {
                        //Shoot is only a coroutine to make sure shooting and animation are synced properly.
                        StartCoroutine(DelayedShoot(shootDelay)); 
                        nextShootTime = Time.time + shootInterval;
                        shootingAnimationEndTime = Time.time + 0.6f; //Ensures that the shooting animation players for at least 0.6 of a second
                    }
                }
                if (Time.time > shootingAnimationEndTime)
                {
                    enemyAnimator.SetBool("isShooting", false);
                }

            }
            enemyAnimator.SetBool("isMoving", false);
        }


    }
    IEnumerator DelayedShoot(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay
        Shoot();
    }
    IEnumerator PlayShootingAnimation(float delay,bool ShootingBool)
    {
        setShooting(ShootingBool);
        yield return new WaitForSeconds(delay);
    }
    void setShooting(bool setShooting)
    {
        enemyAnimator.SetBool("isShooting", setShooting);
    }
    void Shoot()
    {

        // Calculate direction from enemy to target
        Vector2 direction = (target.position - transform.position).normalized;

        // Calculate angle for the bullet's rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Instantiate the bullet with the calculated rotation
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.position.x-BulletSpawnLocationX,
            transform.position.y-BulletSpawnLocationY,transform.position.z),
            rotation);

        // Apply velocity to the bullet's Rigidbody2D
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;

        //destory bullet after 10 seconds if not already destroyed
        Destroy(bullet,10f);
    }
    public void TakeDamage(float dmg)
    {
        StartCoroutine(FlashRed());
        health -= dmg;
        healthBar.UpdateHealthBar(health);
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject coin = Instantiate(Resources.Load<GameObject>("Coin"), transform.position, Quaternion.identity);
            //Gold worth from Flyie Shooties
            coin.GetComponent<CoinPickup>().value = 10;
        }
    }
    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;
    }
}
