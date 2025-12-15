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
        Time.timeScale = 1f;
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (endingManager == null) endingManager = FindObjectOfType<EndingCutscene>();

        // Nu mai ascundem panelul aici automat, ca sa nu stricam setarile din editor, 
        // dar ne asiguram ca e oprit daca e cazul.
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
        UnityEngine.Debug.Log("Player Died.");

        // Oprim muzica
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource source in allSources)
        {
            if (source != audioSource) source.Stop();
        }

        // Sunet de moarte
        if (audioSource != null && loseSound != null)
        {
            audioSource.PlayOneShot(loseSound);
        }

        // Dezactivam player-ul
        if (GetComponent<PlayerMovement>() != null) GetComponent<PlayerMovement>().enabled = false;
        if (GetComponent<WeaponController>() != null) GetComponent<WeaponController>().enabled = false;

        // --- AICI ESTE SCHIMBAREA IMPORTANTA ---

        // 1. Incercam sa pornim dialogul de final daca exista
        if (endingManager != null && defeatDialogue != null)
        {
            endingManager.StartEndingDialogue(defeatDialogue);
        }

        // 2. Afisam si Death Screen-ul (panoul negru sau "You Died")
        if (deathScreenPanel != null)
        {
            deathScreenPanel.SetActive(true);
        }

        // 3. Pornim secventa de iesire
        StartCoroutine(QuitGameSequence());
    }

    IEnumerator QuitGameSequence()
    {
        // Oprim timpul CA SA NU MAI POTI JUCA, dar UI-ul ramane pe ecran
        Time.timeScale = 0f;

        // Asteptam 4 secunde (timp real) ca jucatorul sa citeasca dialogul
        yield return new WaitForSecondsRealtime(4f);

        UnityEngine.Debug.Log("Quitting...");
        UnityEngine.Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}