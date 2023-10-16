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
    Vector3 lastPosition;
    void Start()
    {
    
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalLocalPosition = transform.localPosition;  // Store the original local position
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {

        Animator parentAnimator = transform.parent.GetComponent<Animator>();
        ;
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 currentPosition = transform.position;
        AnimatorClipInfo[] clipInfo = parentAnimator.GetCurrentAnimatorClipInfo(0);
        string clipName = "";
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
               // Debug.Log(angle + "ClipName Inside Walkdown Check" + clipName);
                if (angle > -22.5f && angle <= 22.5f)
                {
                    spriteRenderer.sprite = right;
                    // Adjust the y-coordinate relative to the parent:
                    //transform.localPosition = new Vector3(transform.localPosition.x + 0.05f, transform.localPosition.y + 0.05f, transform.localPosition.z);
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
                }
                else if (angle <= -110 && angle > -165f)
                {
                    Debug.Log("entering diagonal left");
                    spriteRenderer.sprite = diagonalLeft;
                    transform.position = new Vector3(transform.position.x + 0.08f, transform.position.y + 0.067f, transform.position.z);
                }
                else if (angle > 67.5f && angle <= 112.5f)
                {
                    Debug.Log("down");
                    spriteRenderer.sprite = down;
                    //transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
                }
                else if (angle < -5 && angle >= -75f)
                {
                    spriteRenderer.sprite = diagonalRight;
                    transform.position = new Vector3(transform.position.x + 0.06f, transform.position.y + 0.04f, transform.position.z);
                }
                else if ((angle >= 170 || angle <= -140.5f) || angle > 160)
                {
                    Debug.Log("entering left");
                    spriteRenderer.sprite = left;
                    //transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
                }
                else if (angle > -112.5f && angle <= -67.5f)
                {
                    spriteRenderer.sprite = down;
                    //transform.position = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
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
                else if (angle > -67.5f && angle <= -22.5f)
                {
                    // Not needed as the character can't look backward
                }
                else if (angle > -112.5f && angle <= -67.5f)
                {
                    // Not needed as the character can't look backward
                }
                else if (angle > -157.5f && angle <= -112.5f)
                {
                    spriteRenderer.sprite = diagonalLeft1;
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

                //look up
                if (angle > 90f && angle <= 124f)
                {
                    spriteRenderer.sprite = Up2;
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
                }
                else if (angle > 124f && angle <= 170f)
                {
                    spriteRenderer.sprite = diagonalUp1;
                }
                else if (angle > 170f || angle <= -170f)
                {
          
                spriteRenderer.sprite = left2;
                }
                else if (angle > -125f && angle <= -70f)
                {
                    spriteRenderer.sprite = down2;
                    transform.position = new Vector3(transform.position.x + 0.03f, transform.position.y - 0.05f, transform.position.z);
                }
                else if (angle > -160f && angle <= -115f)
                {
                    spriteRenderer.sprite = diagonalDown2;
                }
                //if mouse is behind character
                else if(angle < 70f || angle >= -70f)
                {
                Debug.Log("MOUSE BEHIND CHARACTER");
                spriteRenderer.sprite = left2;
                    //transform.position = new Vector3(transform.position.x + 0.03f, transform.position.y - 0.05f, transform.position.z);
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
            Debug.Log(angle + "ClipName Inside WalkRight" + clipName);

            // Reset the local position back to the original local position
            transform.localPosition = originalLocalPosition;

            //look up
            if (angle < 110f && angle >= 60f)
            {
                spriteRenderer.sprite = Up3;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);
            }
            //look diagonal up
            else if (angle <60f && angle >= 25f)
            {
                spriteRenderer.sprite = diagonalUp3;
            }
            //look right
            else if (angle <25f  && angle >= -25f)
            {
                spriteRenderer.sprite = right3;
                transform.position = new Vector3(transform.position.x, transform.position.y - 0.04f, transform.position.z);
            }
            //look diagonal down
            else if (angle < -25f && angle > -70f)
            {
                spriteRenderer.sprite = diagonalDown3;
                transform.position = new Vector3(transform.position.x + 0.07f, transform.position.y - 0.04f, transform.position.z);
            }
            //look down
            else if (angle < -60f && angle >= -110f)
            {
                spriteRenderer.sprite = down3;
                transform.position = new Vector3(transform.position.x - 0.03f, transform.position.y - 0.10f, transform.position.z);
            }
            else if (angle > -90f && angle <= -45f)
            {
                // Not needed as the character can't look backward
            }
            else if (angle > -135f && angle <= -90f)
            {
                // Not needed as the character can't look backward
            }
            //else if (angle > -180f && angle <= -135f)
            //{
            //    spriteRenderer.sprite = diagonalDown2;
            //}
        }



        // }
    }
}
