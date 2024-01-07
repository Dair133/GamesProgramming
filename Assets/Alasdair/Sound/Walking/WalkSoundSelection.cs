using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WlakSoundSelection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject playerReference;
    private PlayerController playerController;
    public bool stone;
    public bool grass;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        playerReference = GameObject.FindGameObjectWithTag("PlayerTag");
        playerController = playerReference.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        Debug.Log("Changing Walk Type");
        if (stone)
        {
            playerController.stone = true;
            playerController.grass = false;
        }
        else if(grass)
        {
            playerController.grass = true;
            playerController.stone = false;
        }


    }
}
