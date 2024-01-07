using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletDamage = -1;
    
    private AudioSource bulletSoundSource;
    public AudioClip bulletSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        bulletSoundSource = GetComponent<AudioSource>();
        bulletSoundSource.PlayOneShot(bulletSoundClip);
        //if damage not set in public variable then set it manually
        if(bulletDamage == -1)
        {
            bulletDamage = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bullet has collided with an enemy
        if (other.tag.Equals("PlayerTag"))
        {
            //gets script from player which handles damage and healing(not supposed to be named
            //"playerScripts" but changing it causes errors
            PlayerScripts dmgScript = other.GetComponent<PlayerScripts>();

            //calls the take dmg function from that scrips which updates ui and player health value
            dmgScript.TakeDamage(bulletDamage);

            // Destroy the bullet
            Destroy(gameObject);
        }
       
    }
}
