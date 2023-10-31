using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftArm : MonoBehaviour
{


    public GameObject player;
    public GameObject head;

    private GameObject currentArm;

    public int rotationSpeed;
  

    SpriteRenderer leftArmSprite;
    SpriteRenderer headSprite;
  
    private float maxMouseAngle;
    private float minMouseAngle;

    private Vector3 originalPosition;
    private Vector3 lastMousePosition;

    private PlayerController playerControllerScript;

    private string clipName;

    private int clipChoice;
    private int previousClip;


    void Start()
    {
        leftArmSprite = this.gameObject.GetComponent<SpriteRenderer>();
        originalPosition = transform.localPosition;
        headSprite = head.GetComponent<SpriteRenderer>();
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
        previousClip = playerControllerScript.InitializeClip();
    }
    void Update()
    {

        Vector3 mousePos = Input.mousePosition;

        //if (Vector3.Distance(lastMousePosition, mousePos) > 0.015f) ;
        ////do nothing
        //else
        //    return;

        clipName = headSprite.sprite.name;

        clipChoice = playerControllerScript.InitializeClip();

     
        if (playerControllerScript.isHoldingObject == true)
        {
           holdingObject();
        }
        else if(playerControllerScript.isHoldingLargeWeapon == true)
        {
            holdingLargeWeapon(mousePos);
        }
        else if(playerControllerScript.isHoldingMeleeWeapon == true)
        {
            holdingMeleeWeapon();
        }
        
        else
        {
            
            holdingNothing(mousePos);
        }
    }
    void holdingMeleeWeapon()
    {
        if(clipChoice == 3)
        {
            transform.localScale = new Vector3(1f, 1.1f, 0f);
            transform.localRotation = Quaternion.AngleAxis(0f, Vector3.forward);
            if (Input.GetMouseButtonDown(0))
            {
                
                StartCoroutine(MoveArm());
            }

        }



    }
    void holdingObject()
    {
        if (clipChoice != 2)
        {
            leftArmSprite.sortingOrder = 2;
        }

        if (clipChoice == 0)//left
        {
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            transform.rotation = Quaternion.AngleAxis(320f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1.1f, 0f);
        }
       if(clipChoice == 1)//right
        {
            transform.localPosition = new Vector3(originalPosition.x + 0.35f, originalPosition.y, originalPosition.z);
            transform.rotation = Quaternion.AngleAxis(50f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1.1f, 0f);
        }
       if(clipChoice == 2)//up
        {
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            transform.localScale = new Vector3(1f, 1f, 0f);
            leftArmSprite.sortingOrder = -2;
        }
        
        if(clipChoice == 3)//down
        {
            transform.localPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z);
            transform.rotation = Quaternion.AngleAxis(50f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1f, 0f);
        }


    }










    public Vector3 pivotOffset = new Vector3(0, 0.2f, 0);  // Offset of the pivot point from the transform position
    // Update is called once per frame
    
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

        // Rotate the arm to face the mouse pointer
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);

        //weaponObject.transform.rotation = Quaternion.AngleAxis(angle - 12f, Vector3.forward);

        lastMousePosition = mousePos;


    }
    void holdingLargeWeapon(Vector3 mousePos)
{
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    mousePosition.z = 0;
    Vector3 pivotPoint = transform.position + transform.TransformVector(pivotOffset);
    Vector3 direction = (mousePosition - player.transform.position).normalized;
    direction.z = 0;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    /*
     * ARM ANIMATIONS WHEN FACING LEFT
     */
    if (clipChoice == 0) 
    {
        //any code which needs to be run once when the animation switched can be done here
        if (previousClip != 0) 
        {
            leftArmSprite.sortingOrder = 2;
        }
        previousClip = 0;

        //first half of rotation 
        if (angle < -105 && angle > -180) 
        {
            //the scalefactors and math.lerp/inverselerp are used to make the animation smooth and ar basically just "as one value increases change another value smoothly"
            //below its "as angle changes from -110 to -180 change the angle from 100 to 110"
            //same logic applies to all instances where this appears
            float scaleFactor = Mathf.Lerp(angle + 100f, angle + 110f, Mathf.InverseLerp(-110f, -180f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            float scaleFactor2 = Mathf.Lerp(1.1f, 1.4f, Mathf.InverseLerp(-90f, -120f, angle));
            transform.localScale = new Vector3(1f, scaleFactor2, 0f);
        } 
        //second half
        else if (angle < 180 && angle - 25 > 75) 
        {
            float scaleFactor = Mathf.Lerp(angle + 110f, angle + 110f, Mathf.InverseLerp(180f, 75f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            float scaleFactor2 = Mathf.Lerp(1.1f, 1.4f, Mathf.InverseLerp(60f, 100f, angle));
            transform.localScale = new Vector3(1f, scaleFactor2, 0f);

            float scaleFactor4 = Mathf.Lerp(originalPosition.x, originalPosition.x - 0.15f, Mathf.InverseLerp(-90, -180f, angle));
            transform.localPosition = new Vector3(scaleFactor4, originalPosition.y, 0);
        }
    }

        /*
         * ARM ANIMATIONS WHEN FACING RIGHT
        */
        if (clipChoice == 1)
    {
        if (previousClip != 1) 
        {
            leftArmSprite.sortingOrder = 2;
        }
        previousClip = 1;

        if (angle < 75 && angle >= -75) 
        {
            float scaleFactor = Mathf.Lerp(angle + 80f, angle + 90f, Mathf.InverseLerp(-90f, 0f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            float scaleFactor2 = Mathf.Lerp(1.1f, 1.5f, Mathf.InverseLerp(-90f, 0f, angle));
            transform.localScale = new Vector3(1f, scaleFactor2, 0f);

            float scaleFactor4 = Mathf.Lerp(originalPosition.x, originalPosition.x + 0.25f, Mathf.InverseLerp(-90, 0f, angle));
            transform.localPosition = new Vector3(scaleFactor4, originalPosition.y, 0);
        }
    }

        /*
        * ARM ANIMATIONS WHEN FACING UP
        */
        if (clipChoice == 2)
    {
        if (previousClip != 2) 
        {
                //lowers sorting order for when character is up so guns and arm is hidden
            leftArmSprite.sortingOrder = -2;
        }
        previousClip = 2;

        if (angle > 15 && angle <= 90) 
        {
            float scaleFactor = Mathf.Lerp(angle + 80f, angle + 90f, Mathf.InverseLerp(15f, 90f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);
        } 
        else if (angle > 90 && angle < 165) 
        {
            float scaleFactor = Mathf.Lerp(angle + 80f, angle + 90f, Mathf.InverseLerp(90f, 175f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            float scaleFactor2 = Mathf.Lerp(0, -0.35f, Mathf.InverseLerp(90f, 175f, Mathf.Abs(angle)));
           // transform.localPosition = new Vector3(scaleFactor2, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);
        }
    }

        /*
         * ARM ANIMATIONS WHEN FACING DOWN
         */
        if (clipChoice == 3)
    {
        if (previousClip != 3) 
        {
            transform.localPosition = originalPosition;
            leftArmSprite.sortingOrder = 2;
        }
        previousClip = 3;

        if (angle > -60 && angle < 0) 
        {
            transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
            transform.localScale = new Vector3(1f, 1f, 1f);
        } 
        else if (angle < -50) 
        {
            float scaleFactor = Mathf.Lerp(angle + 95f, angle + 120f, Mathf.InverseLerp(-50f, -100f, angle));
            transform.rotation = Quaternion.AngleAxis(scaleFactor, Vector3.forward);

            float scaleFactor2 = Mathf.Lerp(1.1f, 1.4f, Mathf.InverseLerp(-60f, -100f, angle));
            transform.localScale = new Vector3(1f, scaleFactor2, 0f);

            float scaleFactor3 = Mathf.Lerp(1.4f, 1f, Mathf.InverseLerp(-100f, -170f, angle));
            transform.localScale = new Vector3(1f, scaleFactor3, 0f);
        }
    }
       
    }


    IEnumerator MoveArm()
    {
        Quaternion originalRotation = Quaternion.Euler(0, 0, 90); // Set the original angle to -130
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, -90)); // Set the target angle to 180

        float time = 0;
        float speed = 2f;  // Set the speed of the arm movement

        while (time < 1)
        {
            time += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, time);
            yield return null;
        }

        // Return to the original rotation
        transform.rotation = originalRotation;
    }
}
