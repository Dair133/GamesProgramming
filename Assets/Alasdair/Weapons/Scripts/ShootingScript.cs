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
        // Calculate the position where the bullet should be instantiated
        Vector3 spawnPosition = transform.position + transform.TransformDirection(bulletOffset);

        // Create bullet at spawnPosition
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, transform.rotation);

        // Add velocity to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }
}
