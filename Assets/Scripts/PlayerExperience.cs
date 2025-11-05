using UnityEngine;
// using UnityEngine.UI; 

public class PlayerExperience : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToNextLevel = 10;
    public int currentLevel = 1;


    void Start()
    {
        UpdateUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        UnityEngine.Debug.Log($"Player gained {amount} XP. Total: {currentXP}/{xpToNextLevel}");

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    void LevelUp()
    {
        currentLevel++;
        currentXP = 0; 
        xpToNextLevel = (int)(xpToNextLevel * 1.5f); 
        UnityEngine.Debug.Log($"LEVEL UP! Player is now level {currentLevel}. Next level at {xpToNextLevel} XP.");
    }

    void UpdateUI()
    {
        // TODO: Actualizeazã un UI Slider
        // if (xpSlider != null)
        // {
        //     xpSlider.maxValue = xpToNextLevel;
        //     xpSlider.value = currentXP;
        // }
    }
}