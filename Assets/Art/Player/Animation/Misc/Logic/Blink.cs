using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    public Animator animator;  // Attach your GameObject's Animator component here
    private float timeSinceLastBlink = 0f;
    private float nextBlinkTime = 5f;  // Initially set to a lower value

    // Start is called before the first frame update
    void Start()
    {
        // Generate the initial time for the next blink
        nextBlinkTime = Random.Range(2f, 8f);  // Lowered the range for higher frequency
    }

    // Update is called once per frame
    void Update()
    {
        int blinkInt = Random.Range(0, 1000);
        Debug.Log("CHANCE OF BLINKING");
        timeSinceLastBlink += Time.deltaTime;

        // Check if it's time to blink
        if (blinkInt < 1)
        {
            Debug.Log("BLINKING HAS OCCURRED");
            // Trigger the blink animation
            animator.SetTrigger("Blink");

            // Reset the timer and generate a new random time for the next blink
            timeSinceLastBlink = 0f;
            nextBlinkTime = Random.Range(2f, 8f);  // Lowered the range for higher frequency
        }
    }
}
