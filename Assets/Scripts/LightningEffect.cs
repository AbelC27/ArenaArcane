using System;
using UnityEngine;

public class LightningEffect : MonoBehaviour
{
    public int damage = 20;

    public float lifetime = 0.5f;

    void Start()
    {
        Transform target = FindRandomEnemy();

        if (target != null)
        {
            transform.position = target.position;
            EnemyHealth enemy = target.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
        Destroy(gameObject, lifetime);
    }

    Transform FindRandomEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, enemies.Length);
            return enemies[randomIndex].transform;
        }

        return null;
    }
}