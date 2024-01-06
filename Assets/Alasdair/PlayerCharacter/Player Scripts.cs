using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScripts : MonoBehaviour
{
    public float playerHealth = -1; // if not set in inspector
    public float invulnerabilityTime = 0.4f; // Invulnerability time period in seconds

    private bool isInvulnerable = false;

    private GameObject UI;
    private UIManager manager;

    private int coins;

    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("InventoryUI");
        manager = UI.GetComponent<UIManager>();
        
        if (playerHealth == -1)
        {
            playerHealth = 100; // sets hp to 100 if not set manually
        }
    }
    

    public void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            playerHealth -= damage;
            if (UI != null)
            {
                // Updates UI to reflect damage taken
                manager.takeDamage(damage);
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
    
    //Gold pickup function
    public void goldPickup(int amount)
    {
        coins += amount;
        manager.updateCoins(coins);
    }
}
