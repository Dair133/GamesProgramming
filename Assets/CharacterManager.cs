using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        // Create a Vector3 for position
        Vector3 spawnPosition = new Vector3(1527f, 1006f, 0f);

        // Instantiate the player at the specified position with default rotation
        GameObject.Instantiate(player, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
