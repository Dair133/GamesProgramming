using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public bool isMoving;

    public Vector2 input;

    private Animator animator;

    private Vector2 lastInput;

    private string clipName;
    public GameObject head;
    SpriteRenderer headSprite;

    [Header("Bools for selecting animation")]
    public bool isHoldingLargeWeapon = false;
    public bool isHoldingObject = false;
    public bool isHoldingNothing = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        headSprite = head.GetComponent<SpriteRenderer>();

    }
    //bool isRunning = Input.GetKey(KeyCode.LeftShift); // Check if the shift key is being held down

    private void Update()
    {
        clipName = headSprite.sprite.name;
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //*
            //Debug.Log("Values x:"+input.x);
            //Debug.Log("Values y:" + input.y);
            //if input is different than 0 then run something
            //We can remove diagonal movement its done at end of tutorial if we need to remove it.
            if (input != Vector2.zero)
            {
                lastInput = input;
                //Debug.Log("Moving Values x:" + (input.x+input.x).ToString());
                //Debug.Log("Moving Values y:" + (input.y+input.y).ToString());

                //One is added to input values to differntiate between idle and walking animations
                //Idle Left is -1, Idle Up is y=1 but up is y=2
                animator.SetFloat("moveX", input.x+input.x);
                animator.SetFloat("moveY", input.y+input.y);
                animator.SetBool("isWalking", true);

                //inside this variable stored the position of the player
                var targetPos = transform.position;
                targetPos.x += input.x / 10;
                targetPos.y += input.y / 10;

                

                StartCoroutine(Move(targetPos));
            }
            else
            {
                animator.SetBool("isWalking", false);
               // Debug.Log("Player is not moving. Values x:"+lastInput.x+"Values y"+lastInput.y);
                // Set animator parameters to the last direction faced
                animator.SetFloat("moveX", lastInput.x);
                animator.SetFloat("moveY", lastInput.y);
            }

        }


    }
    
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
     public int InitializeClip()
    {



        if (clipName.Contains("left"))
        {
           
            return 0;

        }
        else if (clipName.Contains("right"))
        {
         
            return 1;

        }
        else if (clipName.Contains("up"))
        {
            
            return 2;
            //maybe lower sorting layer of gun here to make it so gun doesnt show through body?
        }
        else//down
        {
            return 3;
        }
    }


    public void ChooseAnimation(GameObject heldItemObject)
    {

        //maybe transfer this section of code to player controller?
        if (heldItemObject.tag.Equals("LargeWeapon"))
        {
            //Debug.Log("Holding large weapon");
            isHoldingLargeWeapon = true;
        }
        else if (heldItemObject.tag.Equals("Object"))
        {
            //Debug.Log("Altering bool for object holding");
           isHoldingObject = true;
           isHoldingLargeWeapon = false;
        }
        else
        {
            // playerControllerScript.isHoldingLargeWeapon = false;
            // playerControllerScript.isHoldingObject = false;
        }


    }


}
