using UnityEngine;
using UnityEngine.UI;

public class PanelColorController : MonoBehaviour
{
    public GameObject[] panels;
    public Color originalColor;
    public Color selectedColor;
    private int selectedPanelIndex = -1;
    
    private void ChangePanelColor(int panelIndex)
    {
        if (selectedPanelIndex == panelIndex)
        {
            panels[panelIndex].GetComponent<Image>().color = originalColor;
            selectedPanelIndex = -1; 
        }
        else
        {
            if (selectedPanelIndex != -1)
            {
                panels[selectedPanelIndex].GetComponent<Image>().color = originalColor;
            }
            panels[panelIndex].GetComponent<Image>().color = selectedColor;
            selectedPanelIndex = panelIndex;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePanelColor(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePanelColor(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangePanelColor(2);
        }
    }
}