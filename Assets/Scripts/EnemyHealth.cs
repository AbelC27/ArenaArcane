using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Loot")]
    public GameObject xpGemPrefab;
    public GameObject healthPotionPrefab; // Drag Potion Prefab here later

    // --- NEW: Slot for the Popup Prefab ---
    [Header("VFX")]
    public GameObject damagePopupPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // --- NEW: Spawn the popup logic ---
        if (damagePopupPrefab != null)
        {
            // Create the popup at the enemy's position
            GameObject popup = Instantiate(damagePopupPrefab, transform.position, Quaternion.identity);

            // Set the number text
            DamagePopup popupScript = popup.GetComponent<DamagePopup>();
            if (popupScript != null)
            {
                popupScript.Setup(damage);
            }
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // 20% Chance to drop a potion if assigned
        if (healthPotionPrefab != null && Random.value < 0.2f)
        {
            Instantiate(healthPotionPrefab, transform.position, Quaternion.identity);
        }
        // Otherwise drop XP
        else if (xpGemPrefab != null)
        {
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}