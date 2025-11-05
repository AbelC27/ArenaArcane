using UnityEngine;

public class XPGem : MonoBehaviour
{
    public int xpAmount = 1; 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExperience playerExp = other.GetComponent<PlayerExperience>();
            if (playerExp != null)
            {
                playerExp.AddXP(xpAmount);
                Destroy(gameObject);
            }
        }
    }
}