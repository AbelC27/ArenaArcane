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

    [Header("Boss Fight Settings")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint; // Place this slightly away from the center
    public BossCutscene cutsceneManager;
    public EnemySpawner enemySpawner; // To stop small enemies

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

            // IF GAME OVER IS FALSE, TRIGGER BOSS INSTEAD OF WINNING
            if (!isGameOver)
            {
                StartBossPhase();
            }
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

    void StartBossPhase()
    {
        isGameOver = true; // Stop the timer logic from running again

        // 1. Kill all existing small enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies)
        {
            Destroy(e);
        }

        // 2. Stop Spawner
        if (enemySpawner != null) enemySpawner.enabled = false;

        // 3. Spawn Boss
        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);

        // 4. Start Cutscene
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (cutsceneManager != null && player != null)
        {
            cutsceneManager.StartBossSequence(boss, player.transform);
        }

    }
    public void BossDefeated()
    {
        HandleWinCondition();
    }
}