using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public GameObject xpGemPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UnityEngine.Debug.Log($"Inamicul a luat {damage} daune, viața rămasă: {currentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (xpGemPrefab != null)
        {
            Instantiate(xpGemPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}