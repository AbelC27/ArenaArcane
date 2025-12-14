using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [Header("Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint; // Where the bullet comes out (mouth/hands)
    public float fireRate = 1.5f;
    public float projectileSpeed = 8f;

    private Transform player;
    private float nextFireTime;

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Face the player (Optional, if you want him to rotate)
        // Vector2 dir = player.position - transform.position;
        // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0, 0, angle);

        // Shooting Logic
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;

            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            EnemyProjectile script = proj.GetComponent<EnemyProjectile>();

            if (script != null)
            {
                script.speed = projectileSpeed;
                script.SetDirection(direction);
            }
        }
    }
}