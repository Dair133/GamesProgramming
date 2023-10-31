using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightArm : MonoBehaviour
{
    public Vector3 pivotOffset = new Vector3(0, 0.2f, 0);  // Offset of the pivot point from the transform position

    /*Following bools determine which animation is played */




   
    public GameObject player;
   // public GameObject head;

    private string clipName;

    private int previousClip;
    private int clipChoice;

    private float prevAngle = 0f; // Store the previous angle
    private float angleThreshold = 1f;

    private Vector3 lastMousePosition;
    private Vector3 originalPosition;

    private PlayerController playerControllerScript;

    private Transform playerTransform;
    private Transform heldItemTransform;
    private GameObject heldItemObject;

    private SpriteRenderer rightArmSprite;


    private void Start()
    {
         rightArmSprite = this.gameObject.GetComponent<SpriteRenderer>();

        lastMousePosition = Input.mousePosition;
        originalPosition = transform.localPosition;
       
       
        //basically gets the parent of the righrArm, so we can do the equivalent of super.yourFunction.
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
    
        //intialise clip is a function of the parent as its being used in all children
        previousClip = playerControllerScript.InitializeClip();


   
    }

   
    void Update()
    {
        player = transform.parent.gameObject;
        playerTransform = transform.parent;
        //originalPosition = transform.localPosition;
        Vector3 mousePos = Input.mousePosition;

        heldItemTransform = playerTransform.GetChild(3);
    
        heldItemObject = heldItemTransform.gameObject;

   
       playerControllerScript.ChooseAnimation(heldItemObject);
        


      
        clipChoice = playerControllerScript.InitializeClip();
      

        if (playerControllerScript.isHoldingLargeWeapon == true)
        {
           holdingLargeWeapon(mousePos);
        }

        else if(playerControllerScript.isHoldingObject == true)
        {
            
            holdingObject();
        }
        else if(playerControllerScript.isHoldingMeleeWeapon == true)
        {
            //Debug.Log("Holding a melee weapon");
            holdingMeleeWeapon();
        }
        else
        {
            holdingNothing(mousePos);
        }
           

    }//end of update function
    void holdingMeleeWeapon()
    {



    }

    void holdingObject()
    {
        if(clipChoice != 2)
        {
            rightArmSprite.sortingOrder = 2;
        }

        if(clipChoice == 0)//left
        {

            //transform.rotation = Quaternion.AngleAxis(70f, Vector3.forward);
            transform.localPosition = new Vector3(originalPosition.x - 0.4f, originalPosition.y, originalPosition.y);
            transform.rotation = Quaternion.AngleAxis(-50f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1.1f, 0f);
        }
        else if(clipChoice == 1)//right
        {
            transform.localPosition = new Vector3(originalPosition.x-0.15f, originalPosition.y, originalPosition.z);
            transform.rotation = Quaternion.AngleAxis(70f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1.1f, 0f);
        }
        else if(clipChoice == 2)
        {
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            transform.rotation = Quaternion.AngleAxis(-50f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1f, 0f);
            rightArmSprite.sortingOrder = -2;
        }

        else if(clipChoice == 3)//down
        {
            //Debug.Log("inside down");
            transform.localPosition = new Vector3(originalPosition.x, transform.localPosition.y, transform.localPosition.z);
            transform.rotation = Quaternion.AngleAxis(-50f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1f, 0f);
        }


     



    }
    void holdingNothing(Vector3 mousePos)
    {

        // Get the world position of the mouse pointer, considering the camera's z position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        mousePosition.z = 0;  // Make sure the mouse's z position is zero

        // Calculate the actual pivot point
        Vector3 pivotPoint = transform.position + transform.TransformVector(pivotOffset);

        // Get the direction from the pivot point to the mouse
        Vector3 direction = (mousePosition - player.transform.position).normalized;
        direction.z = 0;

        // Convert the direction to angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        prevAngle = angle;

        // Check the difference between the current and previous angles
        if (Mathf.Abs(angle - prevAngle) > angleThreshold)
        {
            // Perform your rotation logic here
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

            // Update the previous angle
            prevAngle = angle;
        }

        float clampedAngle = Mathf.Clamp(angle, -180f, 0f);

        lastMousePosition = mousePos;
        // Rotate the arm to face the mouse pointer
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);



    }

    void holdingLargeWeapon(Vector3 mousePos)
    {
        // Get the world position of the mouse pointer, considering the camera's z position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
        mousePosition.z = 0;  // Make sure the mouse's z position is zero

        // Calculate the actual pivot point
        Vector3 pivotPoint = transform.position + transform.TransformVector(pivotOffset);

        // Get the direction from the pivot point to the mouse
        Vector3 direction = (mousePosition - player.transform.position).normalized;
        direction.z = 0;

        // Convert the direction to angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;




        // Rotate the arm to face the mouse pointer
        lastMousePosition = mousePos;
        //Debug.Log(angle + " " + clipChoice);
        
        /*
         * ARM ANIMATIONS WHEN FACING LEFT
        */

        if (clipChoice == 0)
        {
            if (previousClip != 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

            //first half of rotation 
            if (angle < -105)//(was -75)
            {

                float scaleFactor2 = Mathf.Lerp(angle + 70f, angle + 100f, Mathf.InverseLerp(-75, -180f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor2, Vector3.forward);

                float scaleFactor = Mathf.Lerp(1f, 1.45f, Mathf.InverseLerp(-75f, -120f, angle));
                transform.localScale = new Vector3(1f, scaleFactor, 0f);

                float scaleFactor3 = Mathf.Lerp(originalPosition.x, originalPosition.x - 0.25f, Mathf.InverseLerp(-75, -180, angle));
                transform.localPosition = new Vector3(scaleFactor3, originalPosition.y, 0);
            }
            //second half of rotation
            else if (angle >= 110)
            {

                float scaleFactor2 = Mathf.Lerp(angle + 100f, angle + 120f, Mathf.InverseLerp(180, 110f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor2, Vector3.forward);


                float scaleFactor = Mathf.Lerp(1.45f, 1.6f, Mathf.InverseLerp(180f, 110f, angle));
                transform.localScale = new Vector3(1f, scaleFactor, 0f);          
            }
        }

        /*
         * ARM ANIMATIONS WHEN FACING RIGHT
        */

        else if (clipChoice == 1)
        {
            if (previousClip != 1)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

            //first half of rotation
            if (angle > -75 && angle < 0)
            {

                float scaleFactor2 = Mathf.Lerp(angle + 20f, angle + 100f, Mathf.InverseLerp(-75, -0f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor2, Vector3.forward);

                float scaleFactor = Mathf.Lerp(1f, 1.45f, Mathf.InverseLerp(-75f, -10f, angle));
                transform.localScale = new Vector3(1f, scaleFactor, 0f);

          
            }
            else if (angle < 75 && angle > 0)
            {
              
                float scaleFactor2 = Mathf.Lerp(angle + 100f, angle + 120f, Mathf.InverseLerp(0, 75f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor2, Vector3.forward);
            }
        }


        /*
         * ARM ANIMATIONS WHEN FACING UP
        */

        else if (clipChoice == 2)
        {
            if (previousClip != 2)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = -2;
                //transform.position = new Vector3(transform.position.x - 0.3f, weaponObject.transform.position.y, 0);
            }
            if (angle > 15 && angle <= 90)
            {
                float scaleFactor = Mathf.Lerp(angle + 80f, angle + 90f, Mathf.InverseLerp(15f, 90f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);
            }
            else if (angle > 90 && angle < 165)
            {
         
                float scaleFactor = Mathf.Lerp(angle + 80f, angle + 90f, Mathf.InverseLerp(90f, 175f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            }
            lastMousePosition = mousePos;
        }

        /*
        * ARM ANIMATIONS WHEN FACING DOWN
       */

        else if (clipChoice == 3)//down
        {
            if (previousClip != 3)
            {
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            if (angle < -20 && angle > -80)
            {
                transform.rotation = Quaternion.AngleAxis(angle + 70f, Vector3.forward);
                transform.localScale = new Vector3(1f, 1.4f, 0f);
                this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }

            else if (angle > -20 && angle < 0)
            {
                float scaleFactor = Mathf.Lerp(1.4f, 1.0f, Mathf.InverseLerp(-30f, 0f, angle));
                transform.localScale = new Vector3(1f, scaleFactor, 0f);
            }
            else if (angle < -80 && angle > -95f)
            {

                float scaleFactor = Mathf.Lerp(1.4f, 1.6f, Mathf.InverseLerp(-70f, -90f, angle));
                transform.rotation = Quaternion.AngleAxis(angle + 70f, Vector3.forward);
                transform.localScale = new Vector3(1f, scaleFactor, 0f);
            }
            else if (angle < -80)
            {

                transform.rotation = Quaternion.AngleAxis(angle + 110f, Vector3.forward);
                transform.localScale = new Vector3(1f, 1.4f, 0f);

                float scaleFactor = Mathf.Lerp(angle + 70f, angle + 100f, Mathf.InverseLerp(-80f, -120f, angle));
                transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);
            }
            else if (angle < 180 && angle > 90f)
            {
                 if (angle <= 90 && angle > 140)
                {
                   
                    float scaleFactor5 = Mathf.Lerp(originalPosition.x - 0.25f, originalPosition.x, Mathf.InverseLerp(90, 120f, angle));
                    transform.localPosition = new Vector3(scaleFactor5, originalPosition.y, 0);

                }
            }
        }
           
             
              
               




    }//end of holdingLargeWeapon function

}
