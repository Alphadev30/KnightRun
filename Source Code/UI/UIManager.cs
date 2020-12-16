using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text playerGemCountText;
    public Text mainGemCountText;
    public Image selectedButton;
    public Image[] HealthBars;
    

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("UI Manager Not Found");
            }

            return _instance;
        }
    }

    public void Openshop(int gemCount)
    {
        playerGemCountText.text = " " + gemCount + "G";
    }

    public void updateShopSelection(int yPos)
    {
        selectedButton.rectTransform.anchoredPosition = new Vector2(selectedButton.rectTransform.anchoredPosition.x, yPos);

    }

    private void Awake()
    {
        _instance = this;
    }

    public void GemCount(int Count)
    {
        mainGemCountText.text = "" + Count;
    }

    public void UpdateLives(int livesRemaining, bool isHealthplus)
    {
        // Loop through lives
        for (int i = 0; i <= livesRemaining; i++)
        {
            //Do Nothing untill -
            // i == livesRemaining
            // hide that bar
            if (i == livesRemaining && isHealthplus == false)
            {
                HealthBars[i].enabled = false;
            }
            else if (i == livesRemaining && isHealthplus == true)
            {
                HealthBars[i].enabled = true;
            }
        }
    
    }

    public void incrementHealth(int lives)
    {
        for (int i = lives; i >= 0; i--)
        {
            
                HealthBars[i].enabled = true;
            
        }
    }
}
