using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;

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
        // Tell the manager we won!
        if (UpgradeManager.Instance != null)
        {
            UpgradeManager.Instance.BossDefeated();
        }

        // Add explosion VFX here if you want
        Destroy(gameObject);
    }
}