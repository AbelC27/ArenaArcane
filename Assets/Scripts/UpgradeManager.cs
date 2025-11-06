using UnityEngine;
using TMPro; 

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }


    [Header("Game Timer (Mecanica 10)")]
    public float survivalTime; 
    public TextMeshProUGUI timerText; 

    public float timeToWinInMinutes = 20f; 
    private float timeToWinInSeconds; 
    private bool isGameOver = false; 


    [Header("Upgrade UI (Mecanica 8 & 9)")]
    public GameObject upgradeMenu;
    public GameObject auraWeapon;
    public LightningWeapon lightningWeapon;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this; 
        }
        timeToWinInSeconds = timeToWinInMinutes * 60f;
    }

    void Update()
    {
        if (isGameOver) return;
        survivalTime += Time.unscaledDeltaTime;
        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;
        float timeRemaining = timeToWinInSeconds - survivalTime;

        if (timeRemaining <= 0)
        {
            timeRemaining = 0; 
            isGameOver = true; 
            HandleWinCondition(); 
        }
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void HandleWinCondition()
    {
        Time.timeScale = 0f; 
        UnityEngine.Debug.Log("AI CÂȘTIGAT! Ai supraviețuit!");
    }


    public void ShowUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradeMenu.SetActive(true);
    }

    public void HideMenuAndResume()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SelectUpgrade_Aura()
    {
        if (auraWeapon != null)
        {
            auraWeapon.SetActive(true);
        }
        HideMenuAndResume();
    }

    public void SelectUpgrade_Lightning()
    {
        if (lightningWeapon != null)
        {
            lightningWeapon.enabled = true;
        }
        HideMenuAndResume();
    }

    public void SelectUpgrade_MoveSpeed()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.moveSpeed *= 1.10f;
        }
        HideMenuAndResume();
    }

    public void SelectUpgrade_Cooldown()
    {
        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.cooldownReduction += 0.05f;
        }
        HideMenuAndResume();
    }
}