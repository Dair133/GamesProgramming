using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    //Health variables
    private AudioSource audioSource;
    public AudioClip potionSound;

    public Image healthBar;
    public float health = 100f;
    private PlayerScripts playerHealthScript;
    
    //Potion variables
    public int potions;
    public TextMeshProUGUI potionText;
    
    //Gold variables
    public TextMeshProUGUI goldText;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(potionSound);
            playerHealthScript.Heal(5);

            //reflects new health in UI for user
            heal(5);
            potions -= 1;
            updatePotions();
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
    
    //Coin options
    public void updateCoins(int coins)
    {
        goldText.text = coins.ToString();
    }

    public void addPotion()
    {
        potions += 1;
        updatePotions();
    }

    public void updatePotions()
    {
        potionText.text = "x" + potions;
    }
}