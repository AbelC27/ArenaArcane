using UnityEngine;
using TMPro; 

public class PlayerGold : MonoBehaviour
{
    public int currentGold = 0;
    public TextMeshProUGUI goldText; 

    void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + currentGold.ToString();
        }
    }
}