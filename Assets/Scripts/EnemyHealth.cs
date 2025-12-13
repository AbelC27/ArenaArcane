using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    [Header("Loot")]
    public GameObject xpGemPrefab;
    public GameObject healthPotionPrefab; // Drag Potion Prefab here later

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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