using System.Collections;
using System.Collections.Generic;
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
    public GameObject deathScreenPanel;
    public EndingCutscene endingManager;
    public Dialogue defeatDialogue;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip loseSound;
    public AudioSource backgroundMusic;
    public AudioClip hitSound;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (endingManager == null) endingManager = FindObjectOfType<EndingCutscene>();

        if (deathScreenPanel == null)
        {
            GameObject panel = GameObject.Find("DeathPanel");
            if (panel != null) deathScreenPanel = panel;
        }
        if (deathScreenPanel != null) deathScreenPanel.SetActive(false);

        if (audioSource == null) audioSource = GetComponent<AudioSource>();

        if (backgroundMusic == null)
        {
            GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");
            if (cam != null) backgroundMusic = cam.GetComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth > 0 && audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

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
        UnityEngine.Debug.Log("Player has died!");

        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            if (source != audioSource)
            {
                source.Stop();
            }
        }
        if (GetComponent<PlayerMovement>() != null) GetComponent<PlayerMovement>().enabled = false;
        if (GetComponent<WeaponController>() != null) GetComponent<WeaponController>().enabled = false;

        if (audioSource != null && loseSound != null)
        {
            audioSource.Stop(); 
            audioSource.PlayOneShot(loseSound);
        }
        Time.timeScale = 0f;

        if (deathScreenPanel != null)
        {
            deathScreenPanel.SetActive(true);
        }

        if (endingManager != null && defeatDialogue != null)
        {
            endingManager.StartEndingDialogue(defeatDialogue);
        }
        else
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}