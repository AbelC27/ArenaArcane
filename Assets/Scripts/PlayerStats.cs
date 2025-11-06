using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Movement")]
    public float moveSpeed = 5f; 

    [Header("Weapon Stats")]
    public float damageMultiplier = 1f; 
    public float cooldownReduction = 0f; 

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}