using UnityEngine;

public class Projectile : MonoBehaviour
{
    // We hide these because they are now set by the WeaponController, not the Inspector
    [HideInInspector] public float speed;
    [HideInInspector] public float lifetime;
    [HideInInspector] public int damage;

    private Vector2 moveDirection;

    void Start()
    {
        // Safety check: if lifetime wasn't set, default to 3s
        if (lifetime <= 0) lifetime = 3f;
        Destroy(gameObject, lifetime);
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        // Use the speed variable that was set by the controller
        GetComponent<Rigidbody2D>().linearVelocity = moveDirection * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we hit is an Enemy OR a Boss
        // (Make sure your Boss object has the tag "Enemy" or add a check for "Boss" tag if you use that)
        if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
        {
            // Try to get EnemyHealth
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Try to get BossHealth
            BossHealth boss = other.GetComponent<BossHealth>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}