using UnityEngine;
using System.Collections;

public class SwordMovement : MonoBehaviour
{
    public float swingSpeed = 1.3f;
    private PlayerController playerControllerScript;
    Vector3 originalPosition;
    public GameObject leftArm;
    int clipChoice;

    private void Start()
    {
        playerControllerScript = transform.parent.GetComponent<PlayerController>();
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        clipChoice = playerControllerScript.InitializeClip();


        Debug.Log("melee weapon script" + clipChoice);
        if (clipChoice == 3)
        {
            transform.localPosition = new Vector3(originalPosition.x - 0.35f, originalPosition.y - 0.4f, originalPosition.z);
        }

        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            StartCoroutine(SwingSwordDown());
        }
    }

    IEnumerator SwingSwordDown()
    {
        // Assuming swordSprite is your SpriteRenderer component.
        SpriteRenderer swordSprite = this.gameObject.GetComponent<SpriteRenderer>();

        // Make the sword visible at the start of the swing
        swordSprite.enabled = true;

        Quaternion originalRotation = Quaternion.Euler(0, 0, 130); // Set the original angle to -130
        Quaternion targetRotation = Quaternion.Euler(0, 0, -20);  // Set the target angle to 20

        Vector3 originalPosition = transform.localPosition; // Store the original local position
        Vector3 targetPosition = originalPosition + new Vector3(0.3f, 0, 0); // Target position is 0.3 units ahead in x

        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * swingSpeed;

            // Interpolate the rotation
            transform.rotation = Quaternion.Lerp(originalRotation, targetRotation, time);

            // Interpolate the position
            transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, time);

            yield return null;
        }

        // Return to the original rotation and position
        transform.rotation = originalRotation;
        transform.localPosition = originalPosition;

        // Make the sword invisible after the swing
        swordSprite.enabled = false;
    }

}
