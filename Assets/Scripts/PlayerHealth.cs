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

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
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

    // --- NEW FUNCTION ---
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
        UnityEngine.Debug.Log("Player Healed!");
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
        // Add this block to update text
        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Player has died!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}