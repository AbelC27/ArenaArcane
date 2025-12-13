using UnityEngine;
using UnityEngine.UI; 

public class PlayerExperience : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 3; 

    public UpgradeManager upgradeManager;

    public Slider xpSlider;

    void Start()
    {
        UpdateXPUI();
    }

    public void AddXP(int amount)
    {
        currentXP += amount;
        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        UpdateXPUI();
    }

    void LevelUp()
    {
        currentLevel++;
        xpToNextLevel = (int)(xpToNextLevel * 1.5f); 

        if (xpSlider != null)
        {
            xpSlider.maxValue = xpToNextLevel;
        }

        UnityEngine.Debug.Log($"LEVEL UP! Level {currentLevel}");

        if (upgradeManager != null)
        {
            upgradeManager.ShowUpgradeMenu();
        }
    }

    void UpdateXPUI()
    {
        if (xpSlider != null)
        {
            if (xpSlider.maxValue != xpToNextLevel)
            {
                xpSlider.maxValue = xpToNextLevel;
            }
            xpSlider.value = currentXP;
        }
    }
}