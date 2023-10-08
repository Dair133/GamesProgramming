using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    public bool isMoving;

    public Vector2 input;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            


            //if input is different than 0 then run something
            //We can remove diagonal movement its done at end of tutorial if we need to remove it.
            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                //inside this variable stored the position of the player
                var targetPos = transform.position;
                targetPos.x += input.x / 6;
                targetPos.y += input.y / 6;

                

                StartCoroutine(Move(targetPos));
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


}
