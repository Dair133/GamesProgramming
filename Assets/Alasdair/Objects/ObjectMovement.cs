using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private int clipChoice;
    private Vector3 originalPosition;
    private SpriteRenderer objectSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = transform.parent.GetComponent<PlayerController>();

        

       objectSprite = this.gameObject.GetComponent<SpriteRenderer>();

        originalPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
        adjustObjectPosition();
    }

    void adjustObjectPosition()
    {


        clipChoice = playerControllerScript.InitializeClip();

        if(clipChoice != 2)
        {
            objectSprite.sortingOrder = 3;
        }
        
        if (clipChoice == 0)//left
        {
      
            transform.localPosition = new Vector3(originalPosition.x - 0.4f, originalPosition.y-0.2f, originalPosition.z);
        }
        if (clipChoice == 1)//right
        {
            transform.localPosition = new Vector3(originalPosition.x + 0.35f, originalPosition.y - 0.2f, originalPosition.z);
        }
        if (clipChoice == 2)//up
        {
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            objectSprite.sortingOrder = -2;
        }
        if (clipChoice == 3)//down
        {
            transform.localPosition = new Vector3(originalPosition.x,originalPosition.y - 0.2f, originalPosition.z);
        }


    }
}
