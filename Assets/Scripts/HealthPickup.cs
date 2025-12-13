using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the thing we touched is the Player
        if (collision.CompareTag("Player"))
        {
            // Get the player's health script
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            // If the player has a health script, heal them!
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
                Destroy(gameObject); // The potion disappears after use
            }
        }
    }
}