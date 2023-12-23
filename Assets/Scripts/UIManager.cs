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
    
    // Update is called once per frame
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
