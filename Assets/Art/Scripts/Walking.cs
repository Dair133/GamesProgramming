using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float moveX = animator.GetFloat("moveX");
        float moveY = animator.GetFloat("moveY");

        // Logic for walking down
        if (moveY == -2)
        {
            Debug.Log("Character is walking down.");
            // You can add any additional logic for walking down here
        }
        
        /*
        // Logic for walking up
        if (moveY == 2)
        {
            Debug.Log("Character is walking up.");
            // You can add any additional logic for walking up here
        }

        // Logic for walking left
        if (moveX == -2)
        {
            Debug.Log("Character is walking left.");
            // You can add any additional logic for walking left here
        }

        // Logic for walking right
        if (moveX == 2)
        {
            Debug.Log("Character is walking right.");
            // You can add any additional logic for walking right here
        }
        */
    }
}
