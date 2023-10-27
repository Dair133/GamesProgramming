using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float moveX = animator.GetFloat("moveX");
        float moveY = animator.GetFloat("moveY");

        // Logic for idling animations
        if (moveX == 0 && moveY == 0)
        {
            // Character is in idle state, do nothing
            // You might play an idle animation here
            //Debug.Log("Character is idle.");
        }
        // Transition logic to walking animations
    
    }
}
