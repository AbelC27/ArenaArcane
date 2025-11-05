using System.Collections.Generic;
using UnityEngine;

public class AuraWeapon : MonoBehaviour
{
    public int damage = 1;

    public float damageInterval = 1f;

    private Dictionary<EnemyHealth, float> enemiesInAura = new Dictionary<EnemyHealth, float>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null && !enemiesInAura.ContainsKey(enemy))
            {
                enemiesInAura.Add(enemy, 0f);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            if (enemy != null && enemiesInAura.ContainsKey(enemy))
            {
                float newTimer = enemiesInAura[enemy] - Time.deltaTime;
                enemiesInAura[enemy] = newTimer;

                if (newTimer <= 0f)
                {
                    enemy.TakeDamage(damage);
                    enemiesInAura[enemy] = damageInterval;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null && enemiesInAura.ContainsKey(enemy))
            {
                enemiesInAura.Remove(enemy);
            }
        }
    }
}