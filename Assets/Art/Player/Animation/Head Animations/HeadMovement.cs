using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollowMouse : MonoBehaviour
{
    [Header("Down Animations")]
    public Sprite down;
    public Sprite left;
    public Sprite diagonalLeft;
    public Sprite right;
    public Sprite diagonalRight;

    [Header("Up Animations")]
    public Sprite Up1;
    public Sprite left1;
    public Sprite diagonalLeft1;
    public Sprite right1;
    public Sprite diagonalRight1;

    [Header("Left Animations")]
    public Sprite Up2;
    public Sprite left2;
    public Sprite diagonalDown2;
    public Sprite down2;
    public Sprite diagonalUp1;

    [Header("Right")]
    public Sprite Up3;
    public Sprite right3;
    public Sprite diagonalDown3;
    public Sprite down3;
    public Sprite diagonalUp3;


    private SpriteRenderer spriteRenderer;
    private Vector3 originalPosition;
    private Vector3 originalLocalPosition;  // Variable to store the original local position
    private Vector3 lastMousePosition;
    private Vector3 previousScale;
    Vector3 lastPosition;
    void Start()
    {
        
        previousScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLocalPosition = transform.localPosition;  // Store the original local position
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {

        Animator parentAnimator = transform.parent.GetComponent<Animator>();
      
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 currentPosition = transform.position;
        AnimatorClipInfo[] clipInfo = parentAnimator.GetCurrentAnimatorClipInfo(0);
        string clipName = "";
       

        //this if statment prevents animation flashing from happening when the mouse is not moving but the mouse position is 
        //changing because of unity float variation i.e. mouse pos switching between 79.2 and 80.1 therefore animation flashing
        if (Vector3.Distance(lastMousePosition, mousePos) > 0.1f);
        //do nothing
        else 
            return;
      

        if (clipInfo.Length > 0)
        {
            AnimationClip currentClip = clipInfo[0].clip;
            clipName = currentClip.name;
            //Debug.Log("Current clip name: " + clipName);
        }

       // if (mousePos != lastMousePosition || lastPosition != currentPosition)
       //  {
            if (clipName.Equals("IdleDown") || clipName.Equals("WalkDown"))
            {
                lastMousePosition = mousePos;
                lastPosition = currentPosition;
                Vector2 direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
            );

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Reset the local position back to the original local position
                transform.localPosition = originalLocalPosition;
                transform.localScale = previousScale;
               // Debug.Log(angle + "ClipName Inside Walkdown Check" + clipName);
                if (angle > -22.5f && angle <= 22.5f)
                {
                    spriteRenderer.sprite = right;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
                }
                else if (angle < -5 && angle >= -75f)
                {
                    spriteRenderer.sprite = diagonalRight;
                    transform.position = new Vector3(transform.position.x + 0.06f, transform.position.y + 0.04f, transform.position.z);
                }
                 else if (angle > -112.5f && angle <= -67.5f)
                 {
                    spriteRenderer.sprite = down;
                 }
                else if (angle <= -112.5f && angle > -165f)
                {
                    spriteRenderer.sprite = diagonalLeft;
                    transform.position = new Vector3(transform.position.x + 0.08f, transform.position.y + 0.067f, transform.position.z);
                }
                else if ((angle >= 165 || angle <= -150.5f) || angle > 160)
                {
                  
                    spriteRenderer.sprite = left;
                }
                //both of these are for when the mouse is behind the character
                else if (angle > 22f && angle <= 90f)
                {
                    spriteRenderer.sprite = right;
                }
                else if (angle > 90 && angle < 165f)
                {
                    spriteRenderer.sprite = left;
                }
                //statment without if statment to ensure that a down animation plays
                //this is to compensate for if the mouse is at an angle and the character switches between differnt directions
                //and accidentaly enters an angle which doesnt have an animation for that angle on that direction
                //thus playing the down animation when the charater is facing up i.e. playing wrong animation for wrong direction
                else
                {
                    spriteRenderer.sprite = down;
                }

            
            
        }
            if (clipName.Equals("IdleUp") || clipName.Equals("WalkUp"))
            {
                lastMousePosition = mousePos;
                Vector2 direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
                );

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Debug.Log(angle + "ClipName Inside Idle Up" + clipName);
                // Reset the local position back to the original local position
                transform.localPosition = originalLocalPosition;
                transform.localScale = previousScale;
                 //if statments are head moving from left to right
                if (angle > 157.5f || angle <= -157.5f)
                {
                    spriteRenderer.sprite = left1;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                }
                else if (angle > 112.5f && angle <= 157.5f)
                {
                    spriteRenderer.sprite = diagonalLeft1;
                }
                else if (angle > 67.5f && angle <= 112.5f)
                {
                    spriteRenderer.sprite = Up1;  // Character looking directly up
                }
                else if (angle > 22.5f && angle <= 67.5f)
                {
                    spriteRenderer.sprite = diagonalRight1;
                    transform.position = new Vector3(transform.position.x + 0.07f, transform.position.y - 0.04f, transform.position.z);
                }
                else if (angle > -22.5f && angle <= 22.5f)
                {
                    spriteRenderer.sprite = right1;
                    transform.position = new Vector3(transform.position.x+0.03f, transform.position.y - 0.05f, transform.position.z);
                }
                //if mouse is behind character
                else if(angle < -22.5f && angle > -90f)
                {
                    spriteRenderer.sprite = right1;
                    transform.position = new Vector3(transform.position.x + 0.03f, transform.position.y - 0.05f, transform.position.z);
            }
                else if (angle > -160.5f && angle <= -90f)
                {
                    spriteRenderer.sprite = left1;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                 }
                //ensure up animation always plays when facing up
                else
                {
                    spriteRenderer.sprite = Up1;
                }

        }
            if (clipName.Equals("WalkLeft") || clipName.Equals("IdleLeft"))
            {
                lastMousePosition = mousePos;
                Vector2 direction = new Vector2(
                mousePos.x - transform.position.x,
                mousePos.y - transform.position.y
                );

                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Debug.Log(angle + "ClipName Inside WalkLeft" + clipName);

                // Reset the local position back to the original local position
                transform.localPosition = originalLocalPosition;
                transform.localScale = previousScale;
                //look up
                if (angle > 90f && angle <= 115f)
                {
                    spriteRenderer.sprite = Up2;
                    transform.position = new Vector3(transform.position.x - 0.045f, transform.position.y - 0.065f, transform.position.z);
                    transform.localScale = new Vector3(0.78f, 1.0f, 1.0f);
                }
                else if (angle > 115f && angle <= 162f)
                {
                    spriteRenderer.sprite = diagonalUp1;
                }
                else if (angle > 162f || angle <= -162f)
                {
                    spriteRenderer.sprite = left2;
                }
                else if (angle > -162f && angle <= -115f)
                {
                    spriteRenderer.sprite = diagonalDown2;
                }
                else if (angle > -115f && angle <= -90f)
                {
                    spriteRenderer.sprite = down2;
                    transform.position = new Vector3(transform.position.x + 0.015f, transform.position.y - 0.05f, transform.position.z);
                }
               
                //when mouse behind character
                else if(angle >90 || angle >= 0f)
                {
                    spriteRenderer.sprite = Up2;
                    transform.position = new Vector3(transform.position.x - 0.045f, transform.position.y - 0.065f, transform.position.z);
                    transform.localScale = new Vector3(0.78f, 1.0f, 1.0f);

            }
                else if (angle < 0f || angle >= -90f)
                {

                    spriteRenderer.sprite = down2;
                    transform.position = new Vector3(transform.position.x + 0.015f, transform.position.y - 0.05f, transform.position.z);

            }
            else
                {
                    spriteRenderer.sprite = left2;
                }
        }
            if (clipName.Equals("WalkRight") || clipName.Equals("IdleRight"))
            {
            lastMousePosition = mousePos;
            Vector2 direction = new Vector2(
            mousePos.x - transform.position.x,
            mousePos.y - transform.position.y
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float clampedAngle = Mathf.Clamp(angle, -180f, 180f);
            Debug.Log(angle + "ClipName Inside WalkRight" + clipName);

            // Reset the local position back to the original local position
            transform.localPosition = originalLocalPosition;
            transform.localScale = previousScale;
            //look up
            if (angle < 90f && angle >= 65f)
            {
                spriteRenderer.sprite = Up3;
                transform.position = new Vector3(transform.position.x-0.045f, transform.position.y - 0.08f, transform.position.z);
                transform.localScale = new Vector3(0.78f, 1.0f, 1.0f);  // Scales the object to 110%
            }
            //look diagonal up
            else if (angle <65f && angle >= 10f)
            {
                spriteRenderer.sprite = diagonalUp3;
            }
            //look right
            else if (angle <15f  && angle >= -23f)//23 instead of 18 to ensure a little overlap prevents flashing between animations
            {
                spriteRenderer.sprite = right3;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z);
            }
            //look diagonal down
            else if (angle < -18f && angle > -70f)
            {
                spriteRenderer.sprite = diagonalDown3;
                transform.position = new Vector3(transform.position.x + 0.07f, transform.position.y - 0.04f, transform.position.z);
            }
            //look down
            else if (angle < -60f && angle >= -90f)
            {
                spriteRenderer.sprite = down3;
                transform.position = new Vector3(transform.position.x - 0.03f, transform.position.y - 0.10f, transform.position.z);
            }
            else if (angle < -90f && angle >= -180)
            {
                spriteRenderer.sprite = down3;
                transform.position = new Vector3(transform.position.x - 0.03f, transform.position.y - 0.10f, transform.position.z);
            }
            else if (angle < 180f && angle >= 90f)
            {
                spriteRenderer.sprite = Up3;
                transform.position = new Vector3(transform.position.x-0.045f, transform.position.y - 0.08f, transform.position.z);
                transform.localScale = new Vector3(0.78f, 1.0f, 1.0f);  
            }
            else
            {
                spriteRenderer.sprite = right3;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z);
            }
         
        }



        // }
    }
}
