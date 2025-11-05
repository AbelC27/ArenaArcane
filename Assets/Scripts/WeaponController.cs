using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData currentWeapon;
    private float cooldownTimer;

    public float detectionRadius = 10f;

    void Start()
    {
        cooldownTimer = 0f;
    }

    void Update()
    {
        if (currentWeapon == null) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            Attack();
            cooldownTimer = currentWeapon.cooldown;
        }
    }

    Transform FindClosestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity; 

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance && distance <= detectionRadius)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }
        return closestEnemy;
    }

    void Attack()
    {
        Transform target = FindClosestEnemy();
        if (target == null)
        {
           
            return;
        }

        Vector2 attackDirection = (target.position - transform.position).normalized;
        GameObject projectileGO = Instantiate(currentWeapon.projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectileGO.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.SetDirection(attackDirection);
            projectileScript.damage = (int)currentWeapon.damage;
        }
    }
}