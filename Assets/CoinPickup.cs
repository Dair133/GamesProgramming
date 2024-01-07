using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int value;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerTag")
        {
            other.GetComponent<PlayerScripts>().goldPickup(value);
            Destroy(gameObject);
        }
    }
}
