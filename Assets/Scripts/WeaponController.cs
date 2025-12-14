using UnityEngine;
using System.Collections.Generic;

public class WeaponController : MonoBehaviour
{
    public WeaponData currentWeapon;
    public WeaponData[] availableWeapons;

    // NEW: Reference to the UI Manager
    public WeaponUIManager uiManager;

    private float cooldownTimer;
    public float detectionRadius = 10f;

    void Start()
    {
        cooldownTimer = 0f;
        // Ensure UI matches startup weapon
        if (uiManager != null) uiManager.UpdateWeaponUI(0);
    }

    void Update()
    {
        // 1. Switch Weapon & Update UI
        if (Input.GetKeyDown(KeyCode.Alpha1) && availableWeapons.Length > 0)
        {
            currentWeapon = availableWeapons[0];
            if (uiManager != null) uiManager.UpdateWeaponUI(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && availableWeapons.Length > 1)
        {
            currentWeapon = availableWeapons[1];
            if (uiManager != null) uiManager.UpdateWeaponUI(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && availableWeapons.Length > 2)
        {
            currentWeapon = availableWeapons[2];
            if (uiManager != null) uiManager.UpdateWeaponUI(2);
        }

        if (currentWeapon == null) return;

        // 2. Cooldown Logic
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0f)
        {
            Attack();
            float reduction = (PlayerStats.Instance != null) ? PlayerStats.Instance.cooldownReduction : 0f;
            cooldownTimer = currentWeapon.cooldown * (1.0f - reduction);
        }
    }

    // ... [Keep the Attack and FindClosestEnemy functions exactly as they were] ...
    void Attack()
    {
        // (Copy your existing Attack logic here from the previous step)
        Transform target = FindClosestEnemy();
        if (target == null) return;

        Vector2 targetDirection = (target.position - transform.position).normalized;
        float startAngle = -currentWeapon.spreadAngle / 2f;
        float angleStep = currentWeapon.projectileCount > 1 ? currentWeapon.spreadAngle / (currentWeapon.projectileCount - 1) : 0;

        for (int i = 0; i < currentWeapon.projectileCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector2 firedDirection = Quaternion.Euler(0, 0, currentAngle) * targetDirection;

            GameObject projectileGO = Instantiate(currentWeapon.projectilePrefab, transform.position, Quaternion.identity);
            Projectile projectileScript = projectileGO.GetComponent<Projectile>();

            if (projectileScript != null)
            {
                projectileScript.damage = (int)currentWeapon.damage;
                projectileScript.speed = currentWeapon.projectileSpeed;
                projectileScript.lifetime = currentWeapon.range;
                projectileScript.SetDirection(firedDirection);
            }
        }
    }

    Transform FindClosestEnemy()
    {
        // (Copy your existing FindClosestEnemy logic here)
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
}