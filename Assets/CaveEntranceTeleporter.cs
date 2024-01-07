using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEntranceTeleporter : MonoBehaviour
{
    public Vector2 teleportDestination; // Assign this in the Inspector

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("Collider triggered");
        if (collision.CompareTag("PlayerTag")) // Ensure your player GameObject has the "Player" tag
        {
            collision.transform.position = teleportDestination; // Teleport the player
           
        }
    }
}
