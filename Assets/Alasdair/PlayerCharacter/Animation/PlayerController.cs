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

    private AudioSource audio;
    public AudioClip grassWalkOne;
    public AudioClip grassWalkTwo;
    public AudioClip stoneWalkOne;
    public AudioClip stoneWalkTwo;
    private AudioClip walkingSoundChosen;
    public bool grass;
    public bool stone;
    private bool walkSoundPlayed;
 


    SpriteRenderer headSprite;

    [Header("Bools for selecting animation")]
    public bool isHoldingLargeWeapon = false;
    public bool isHoldingObject = false;
    public bool isHoldingMeleeWeapon = false;
    public bool isHoldingNothing = false;

    private void Awake()
    {
        grass = true;
        stone = false;
        walkSoundPlayed = false;
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        headSprite = head.GetComponent<SpriteRenderer>();

    }
    //bool isRunning = Input.GetKey(KeyCode.LeftShift); // Check if the shift key is being held down

    private void Update()
    {
        //which walking sound will we choose
        if (grass)
        {
            float randomValue = Random.value;
            if (randomValue < 0.7f)
            {

                walkingSoundChosen = grassWalkOne;
            }
            else
            {
                walkingSoundChosen = grassWalkTwo;
            }
        }
        else if (stone)
        {
            float randomValue = Random.value;
            if (randomValue < 0.7f)
            {
                walkingSoundChosen = stoneWalkOne;
            }
            else
            {
                walkingSoundChosen = stoneWalkTwo; 
            }

        }



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

                Debug.Log(targetPos.ToString() + transform.position.x);
                if (Vector3.Distance(transform.position, targetPos) > 2f)
                {
                    Debug.Log("YILED YIELD YILED");
                    return;
                }
                else
                {
                    if (!walkSoundPlayed)
                    {
                        walkSoundPlayed = true;
                        StartCoroutine(playWalkSound(walkingSoundChosen));
                    }
                   
                    StartCoroutine(Move(targetPos));
                }
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
        int i = 0;
        isMoving = true;

        
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            
            i++;
            if(i < 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            else
            {
                isMoving = false;
                yield break;
            }
            Debug.Log(i);
    
        }

        transform.position = targetPos;
        isMoving = false;
    }
    IEnumerator playWalkSound(AudioClip clip)
    {
        
        audio.PlayOneShot(clip);
        yield return new WaitForSeconds(0.4f);
        walkSoundPlayed = false;
        
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
            //Debug.Log("coohsing");
            //Debug.Log("Holding large weapon");
            isHoldingLargeWeapon = true;
            isHoldingObject = false;
            isHoldingMeleeWeapon = false;
        }
        else if (heldItemObject.tag.Equals("Object"))
        {
            //Debug.Log("Altering bool for object holding");
           isHoldingObject = true;
           isHoldingLargeWeapon = false;
            isHoldingMeleeWeapon = false;
        }
        else if(heldItemObject.tag.Equals("Melee"))
        {
           // Debug.Log("melee weapon selected");
            isHoldingMeleeWeapon = true;
            isHoldingLargeWeapon = false;
            isHoldingObject = false;
        }
        else if(heldItemObject.tag.Equals("Nothing"))
        {
            isHoldingNothing = true;
            isHoldingLargeWeapon = false;
        }
        else
        {
            // playerControllerScript.isHoldingLargeWeapon = false;
            // playerControllerScript.isHoldingObject = false;
        }


    }


}
