using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value;
    private AudioSource audioSource;
    public AudioClip pickUpNoise;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTag")
        {
            audioSource.PlayOneShot(pickUpNoise);
            other.GetComponent<PlayerScripts>().goldPickup(value);
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
