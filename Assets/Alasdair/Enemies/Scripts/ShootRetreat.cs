using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootRetreat : MonoBehaviour
{
    public float retreatSpeed;
    public float followSpeed;
    public Transform target;
    public float AggroRange;
    public float retreatRange;
    public float stopRange;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;
    public float shootInterval = 3f;
    private float nextShootTime;

    // Start is called before the first frame update
    void Start()
    {
        followSpeed = 7;
        retreatSpeed = 8;
        nextShootTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
            if (player != null)
            {
                target = player.GetComponent<Transform>();
            }
        }
        else
        {
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            if (distanceToPlayer < AggroRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, -retreatSpeed * Time.deltaTime);
            }
            else if (distanceToPlayer > AggroRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
            }

            if (Time.time >= nextShootTime)
            {
                Vector2 rayStart = transform.position + (target.position - transform.position).normalized * -0.5f;
                RaycastHit2D hit = Physics2D.Raycast(rayStart, target.position - transform.position, AggroRange);

                if (hit.collider != null)
                {
                   
                  
                        Shoot();
                        nextShootTime = Time.time + shootInterval;
                    
                }
            }
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting the spell");
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = direction * bulletSpeed;
    }
}
