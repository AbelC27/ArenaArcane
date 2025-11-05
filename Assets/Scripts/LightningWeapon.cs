using UnityEngine;

public class LightningWeapon : MonoBehaviour
{
    public GameObject lightningPrefab;
    public float cooldown = 5f;
    private float cooldownTimer;

    void Start()
    {
        cooldownTimer = cooldown;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = cooldown;
        }
    }

    void Attack()
    {
        if (lightningPrefab != null)
        {
            Instantiate(lightningPrefab, transform.position, Quaternion.identity);
        }
    }
}