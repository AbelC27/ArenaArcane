using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int goldAmount = 10; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerGold playerGold = collision.GetComponent<PlayerGold>();

            if (playerGold != null)
            {
                playerGold.AddGold(goldAmount);
                Destroy(gameObject); 
            }
        }
    }
}