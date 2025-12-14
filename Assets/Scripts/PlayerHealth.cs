using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [Header("Death Settings")]
    public GameObject deathScreenPanel;
    public CutsceneManager cutsceneManager;
    public Dialogue defeatDialogue;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip loseSound; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        if (deathScreenPanel != null) deathScreenPanel.SetActive(false);
        if (audioSource == null) audioSource = GetComponent<AudioSource>();
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
        if (currentHealth > maxHealth) currentHealth = maxHealth;
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
        if (audioSource != null && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }

        Time.timeScale = 0f;

        if (deathScreenPanel != null) deathScreenPanel.SetActive(true);

        if (cutsceneManager != null && defeatDialogue != null)
        {
            cutsceneManager.dialogueUI.SetActive(true);
            cutsceneManager.StartDialogue(defeatDialogue);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}