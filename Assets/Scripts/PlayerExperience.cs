using UnityEngine;
// using UnityEngine.UI; 

public class PlayerExperience : MonoBehaviour
{
    public int currentXP = 0;
    public int xpToNextLevel = 10;
    public int currentLevel = 1;
    public UpgradeManager upgradeManager;

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
        xpToNextLevel = (int)(xpToNextLevel * 1.5f);
        UnityEngine.Debug.Log($"LEVEL UP! Player is now level {currentLevel}.");

        if (upgradeManager != null)
        {
            upgradeManager.ShowUpgradeMenu();
        }
        else
        {
            UnityEngine.Debug.LogError("Upgrade Manager nu este conectat la PlayerExperience!");
            Time.timeScale = 0f; 
        }
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