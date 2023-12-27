using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;  // Speed of bullet
    public float fireRate = 0.2f;  // Seconds between shots
    private float nextFireTime = 0f;  // Time of the next shot

    // Position relative to the gun where bullets are fired from
    public Vector3 bulletOffset = new Vector3(0, 1, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        // Calculate the position where the bullet should be instantiated based on local offset
        // Fixed local offset for bullet spawn position
        Vector3 localOffset = new Vector3(1.18f, -0.058f, 0);  // You can set these numbers based on what you need

        Vector3 spawnPosition = transform.TransformPoint(localOffset);

        // Create bullet at spawnPosition
        Quaternion adjustedRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, adjustedRotation);

        // Destroy the bullet after 10 seconds
        Destroy(bullet, 10f);

        // Add force to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(bullet.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }



}
