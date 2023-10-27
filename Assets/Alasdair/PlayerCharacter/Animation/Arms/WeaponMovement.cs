using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponMovement : MonoBehaviour
{

    // Start is called before the first frame update

    private string clipName;
 
    private int clipChoice;
    private int previousClip;

    private Vector3 lastMousePosition;
    private Vector3 startingWeaponPosition;
    private Vector3 lastValidPosition;

    private bool previousFlip;

    public GameObject head;
    private GameObject weaponObject;
    public GameObject player;

    private SpriteRenderer headSprite;
    private SpriteRenderer weaponSprite;

    private PlayerController playerControllerScript;
    void Start()
    {
        startingWeaponPosition = transform.localPosition;
        headSprite = head.GetComponent<SpriteRenderer>();
        weaponSprite = GetComponent<SpriteRenderer>();
        weaponObject = this.gameObject;
        playerControllerScript = transform.parent.GetComponent<PlayerController>();


        previousClip = playerControllerScript.InitializeClip();
        clipName = headSprite.sprite.name;
      
    }


    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        rotateWeapon(mousePos);


    }//end of update


    public Vector3 pivotOffset = new Vector3(0, 0.2f, 0);  // Offset of the pivot point from the transform position
    // Update is called once per frame

    void rotateWeapon(Vector3 mousePos)
    {
        //if (Vector3.Distance(lastMousePosition, mousePos) > 0.015f) ;
        //do nothing
        //   else
        //     return;

        clipChoice = playerControllerScript.InitializeClip();
        clipName = headSprite.sprite.name;



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

        // Calculate the angle of the mouse relative to the weapon
        Vector3 weaponToMouse = mousePosition - weaponObject.transform.position;
        float weaponMouseAngle = Mathf.Atan2(weaponToMouse.y, weaponToMouse.x) * Mathf.Rad2Deg;

        /*LEFT
         * WEAPON MOVEMENT FOR WHEN CHARACTER IS FACING LEFT
         */

        if (clipChoice == 0)
        {
            if (previousClip != 0)
            {

                weaponSprite.flipY = true;
                weaponObject.transform.transform.localPosition = new Vector3(0.2f, -0.13f, 0);
                weaponSprite.sortingOrder = 3;
            }
            previousClip = 0;


            if (angle >= 110 || angle <= -105)
            {

                //we can put this here because weapon is NEVER flipped when character is facing left
                weaponSprite.flipY = true;

                weaponObject.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                weaponObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                if (angle < 180)//very small adjustment to move wepon slightly to left so it looks ok
                {
                    float scaleFactor = Mathf.Lerp(0, -0.027f, Mathf.InverseLerp(180f, 90f, Mathf.Abs(angle)));
                    weaponObject.transform.localPosition = new Vector3(scaleFactor, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);


                }


                lastMousePosition = mousePos;
            }


        }

        /*RIGHT
        * WEAPON MOVEMENT FOR WHEN CHARACTER IS FACING RIGHT
        * 
        */


        if (clipChoice == 1)
        {

            if (previousClip != 1)
            {
                //should also change rotation of gun to a valid rotation for the weapon
                // weaponObject.GetComponent<SpriteRenderer>().flipY = false;
                weaponObject.transform.transform.localPosition = new Vector3(0.2f, -0.13f, 0);
                weaponSprite.sortingOrder = 3;
            }
            previousClip = 0;


            if (angle < 75 && angle >= -75)
            {
                //we can put this here because weapon is ALWAYS flipped when character is facing right
                weaponSprite.flipY = false;

                weaponObject.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                weaponObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                float scaleFactor = Mathf.Lerp(0, -0.14f, Mathf.InverseLerp(180f, 90f, Mathf.Abs(angle)));
                transform.localPosition = new Vector3(scaleFactor, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);
                lastValidPosition = transform.localPosition;



                lastMousePosition = mousePos;
            }//ensures that the weapon position stays consistent even when mouse is outside a valid rotation range
            if ((angle > 75 && angle < 180) || (angle > -180 && angle < -75))
            {
                weaponObject.transform.localPosition = lastValidPosition;
            }






        }


        if (clipChoice == 2)//up
        {
            if (previousClip != 2)
            {
                //should also change rotation of gun to a valid rotation for the weapon
                weaponSprite.flipY = false;

                transform.position = new Vector3(weaponObject.transform.position.x, weaponObject.transform.position.y + 0.7f, 0);
                weaponObject.transform.transform.localPosition = new Vector3(0.2f, -0.13f, 0);

            }

            previousClip = 0;
            weaponSprite.sortingOrder = -2;

            if (angle <= 90 && angle > 15)
            {

                weaponSprite.flipY = false;
                previousFlip = false;
                //weaponObject.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                weaponObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                lastValidPosition = transform.localPosition;



                lastMousePosition = mousePos;
            }
            else if (angle > 90 && angle < 175)
            {

                weaponSprite.flipY = true;
                previousFlip = true;
                //weaponObject.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                weaponObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                float scaleFactor = Mathf.Lerp(0, -0.14f, Mathf.InverseLerp(90f, 175f, Mathf.Abs(angle)));
                weaponObject.transform.localPosition = new Vector3(scaleFactor, weaponObject.transform.localPosition.y, weaponObject.transform.localPosition.z);

                lastValidPosition = transform.localPosition;

                lastMousePosition = mousePos;
            }
            if ((angle > 175 || (angle > -180 && angle < -90)))
            {

                weaponObject.transform.localPosition = lastValidPosition;
                weaponSprite.flipY = previousFlip;
            }
            else if (angle < 15 && angle > -90)
            {

                weaponObject.transform.localPosition = lastValidPosition;
                weaponSprite.flipY = previousFlip;
            }
            //now add datment to maintain last angle

        }


        /*DOWN
       * WEAPON MOVEMENT FOR WHEN CHARACTER IS FACING DOWN
       */

        if (clipChoice == 3)
        {
            if (previousClip != 3)
            {

                weaponSprite.flipY = true;
                weaponObject.transform.transform.localPosition = new Vector3(0.2f, -0.13f, 0);
                weaponSprite.sortingOrder = 3;
            }
            previousClip = 0;
            if (angle <= 0 && angle >= -165)
            {
                Debug.Log("angle: " + angle + "Weapon angle" + weaponMouseAngle);
                // Calculate the x position offset based on the angle, let's say it moves between -0.3 and 0.3
                float xPos = Mathf.Lerp(0.3f, -0.3f, Mathf.Abs(weaponMouseAngle + 180) / 180);

                // New: Calculate the y position offset based on the angle, let's say it moves between -0.15 and -0.08
                float yPos = Mathf.Lerp(-0.15f, -0.08f, Mathf.Abs(weaponMouseAngle + 180) / 180);

                // Set the weapon position

                transform.localPosition = new Vector3(xPos, yPos, 0);

                lastValidPosition = transform.localPosition;

                weaponObject.transform.localScale = new Vector3(0.65f, 0.65f, 1);
                weaponObject.transform.rotation = Quaternion.AngleAxis(angle - 10f, Vector3.forward);

                if (angle < -90)
                {
                    weaponSprite.flipY = true;
                    previousFlip = true;
                }
                else
                {
                    weaponSprite.flipY = false;
                    previousFlip = false;
                }







                lastMousePosition = mousePos;
            }
            if (angle < -165)
            {
                weaponSprite.flipY = previousFlip;
                transform.localPosition = lastValidPosition;
            }
            else if (angle > 0)
            {
                weaponSprite.flipY = previousFlip;

                transform.localPosition = lastValidPosition;

            }

        }
    }

  
}
