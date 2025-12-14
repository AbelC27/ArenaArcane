using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [Header("Audio Settings")]
    public AudioSource musicSource; 
    public AudioSource sfxSource;   
    public AudioClip backgroundMusic;
    public AudioClip winSound;
    public AudioClip upgradeSound;

    [Header("Game Timer")]
    public float survivalTime;
    public TextMeshProUGUI timerText;
    public float timeToWinInMinutes = 20f;
    private float timeToWinInSeconds;
    private bool isGameOver = false;

    [Header("Boss Fight Settings")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public BossCutscene cutsceneManager;
    public EnemySpawner enemySpawner;

    [Header("Upgrade UI")]
    public GameObject upgradeMenu;
    public GameObject auraWeapon;
    public LightningWeapon lightningWeapon;

    [Header("Win Dialogue")]
    public CutsceneManager dialogueSystem;
    public Dialogue winDialogue;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
        timeToWinInSeconds = timeToWinInMinutes * 60f;
    }

    void Start()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true; 
            musicSource.Play();
        }
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
            if (!isGameOver) StartBossPhase();
        }

        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void StartBossPhase()
    {
        isGameOver = true;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in enemies) Destroy(e);

        if (enemySpawner != null) enemySpawner.enabled = false;

        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (cutsceneManager != null && player != null)
        {
            cutsceneManager.StartBossSequence(boss, player.transform);
        }
    }

    public void BossDefeated()
    {
        if (musicSource != null) musicSource.Stop();
        if (sfxSource != null && winSound != null) sfxSource.PlayOneShot(winSound);

        UnityEngine.Debug.Log("BOSS DEAD - WIN SEQUENCE");
        Time.timeScale = 0f;

        if (dialogueSystem != null && winDialogue != null)
        {
            dialogueSystem.dialogueUI.SetActive(true);
            dialogueSystem.StartDialogue(winDialogue);
        }
    }


    public void PlayUpgradeSound()
    {
        if (sfxSource != null && upgradeSound != null)
        {
            sfxSource.PlayOneShot(upgradeSound);
        }
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
        PlayUpgradeSound(); 
        if (auraWeapon != null) auraWeapon.SetActive(true);
        HideMenuAndResume();
    }

    public void SelectUpgrade_Lightning()
    {
        PlayUpgradeSound(); 
        if (lightningWeapon != null) lightningWeapon.enabled = true;
        HideMenuAndResume();
    }

    public void SelectUpgrade_MoveSpeed()
    {
        PlayUpgradeSound(); 
        if (PlayerStats.Instance != null) PlayerStats.Instance.moveSpeed *= 1.10f;
        HideMenuAndResume();
    }

    public void SelectUpgrade_Cooldown()
    {
        PlayUpgradeSound();
        if (PlayerStats.Instance != null) PlayerStats.Instance.cooldownReduction += 0.05f;
        HideMenuAndResume();
    }
}