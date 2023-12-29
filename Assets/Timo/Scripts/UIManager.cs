using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //Health variables
    public Image healthBar;
    public float health = 100f;
    
    //Potion variables
    public int potions;
    
    public TextMeshProUGUI potionText;
   
    private PlayerScripts playerHealthScript;
    // Update is called once per frame

    private void Start()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("PlayerTag");
        playerHealthScript = player.GetComponent<PlayerScripts>();
    }

    void Update()
    {
        //Health Controls
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        //Use a potion
        if (Input.GetKeyDown(KeyCode.Space) && potions >= 1 && health < 100) 
        {
            //updates the actual player class with correct health value
            playerHealthScript.Heal(5);

            //reflects new health in UI for user
            heal(5);
            potions -= 1;
            potionText.text = "x" + potions;
        }
        if(Input.GetKeyDown(KeyCode.Minus))
        {
            takeDamage(10);
        }
    }
    
    //Health functions
    public void takeDamage(float dmg)
    {
        health -= dmg;
        healthBar.fillAmount = health / 100f;
    }

    public void heal(float amt)
    {
        health += amt;
        health = Mathf.Clamp(health, 0, 100);
        healthBar.fillAmount = health / 100f;
    }
}