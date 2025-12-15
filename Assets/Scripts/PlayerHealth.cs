using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Stats")]
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [Header("Death Settings (Lose)")]
    public GameObject deathScreenPanel;   // Ecranul negru
    public EndingCutscene endingManager;  // Scriptul nou pentru final (pune EndingUI aici)
    public Dialogue defeatDialogue;       // Replicile Boss-ului

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip loseSound;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // 1. Găsim automat Ending Manager dacă e gol
        if (endingManager == null)
        {
            endingManager = FindObjectOfType<EndingCutscene>();
            if (endingManager == null) UnityEngine.Debug.LogError("ATENȚIE: Nu am găsit niciun obiect cu scriptul 'EndingCutscene' în scenă!");
        }

        // 2. Găsim automat Death Panel dacă e gol (căutăm după nume)
        if (deathScreenPanel == null)
        {
            GameObject panel = GameObject.Find("DeathPanel");
            if (panel != null) deathScreenPanel = panel;
        }

        // 3. Ascundem ecranul negru la început
        if (deathScreenPanel != null)
        {
            deathScreenPanel.SetActive(false);
        }

        // 4. Găsim sursa audio
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Player has died!");

        // 1. Oprim timpul
        Time.timeScale = 0f;

        // 2. Sunet de înfrângere
        if (audioSource != null && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }

        // 3. Activăm ecranul negru
        if (deathScreenPanel != null)
        {
            deathScreenPanel.SetActive(true);
        }

        // 4. Pornim dialogul de final
        if (endingManager != null && defeatDialogue != null)
        {
            // Apelăm funcția din noul script EndingCutscene
            endingManager.StartEndingDialogue(defeatDialogue);
        }
        else
        {
            // Siguranță: Dacă ai uitat să pui EndingUI, dăm restart jocului
            UnityEngine.Debug.LogWarning("Ending Manager sau Dialogue lipsă! Restarting level...");
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}