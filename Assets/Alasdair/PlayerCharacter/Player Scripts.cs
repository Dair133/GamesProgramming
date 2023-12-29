using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    public float playerHealth = -1; // if not set in inspector
    public float invulnerabilityTime = 0.4f; // Invulnerability time period in seconds

    private bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerHealth == -1)
        {
            playerHealth = 100; // sets hp to 100 if not set manually
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            playerHealth -= damage;
            // Call function to update UI
            GameObject UI = GameObject.FindGameObjectWithTag("InventoryUI");
            UIManager uiManager = UI.GetComponent<UIManager>();
            if (UI != null)
            {
                Debug.Log("TAKING DAMAGE: " + playerHealth);
                // Updates UI to reflect damage taken
                uiManager.takeDamage(damage);
            }

            if (playerHealth <= 0)
            {
                // Do something here upon player death possibly different to how we handle death in UIManager
            }

            StartCoroutine(BecomeInvulnerable());
        }
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
    }

    public void Heal(float healingAmount)
    {
        playerHealth += healingAmount;
    }
}
