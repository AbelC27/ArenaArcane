using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public Transform playerTransform;

    [Tooltip("Closest an enemy can spawn to the player (prevents spawning on top of you)")]
    public float minSpawnRadius = 6f;

    [Tooltip("Furthest an enemy can spawn from the player")]
    public float maxSpawnRadius = 12f;

    public float spawnInterval = 1.0f;

    [Header("Difficulty Scaling")]
    public float timeBetweenDifficultyIncrease = 30f;
    public float spawnIntervalDecrement = 0.05f;
    public float minSpawnInterval = 0.2f;
    private float nextDifficultyIncreaseTime;

    [Header("Map Boundaries")]
    public Vector2 minMapLimit = new Vector2(-20f, -20f);
    public Vector2 maxMapLimit = new Vector2(20f, 20f);

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }

        StartCoroutine(SpawnEnemyRoutine());
        nextDifficultyIncreaseTime = timeBetweenDifficultyIncrease;
    }

    void Update()
    {
        if (UpgradeManager.Instance == null) return;

        // Difficulty Logic
        float currentTime = UpgradeManager.Instance.survivalTime;
        if (currentTime > nextDifficultyIncreaseTime)
        {
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalDecrement, minSpawnInterval);
            nextDifficultyIncreaseTime += timeBetweenDifficultyIncrease;
            Debug.Log($"DIFFICULTY UP! New spawn interval: {spawnInterval}");
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (playerTransform != null && enemyPrefab != null)
            {
                Vector2 spawnPos = Vector2.zero;
                bool validPositionFound = false;

                // Try 10 times to find a position inside the map and near the player
                for (int i = 0; i < 10; i++)
                {
                    // 1. Pick a random direction
                    Vector2 randomDirection = Random.insideUnitCircle.normalized;

                    // 2. Pick a random distance between Min and Max
                    float randomDistance = Random.Range(minSpawnRadius, maxSpawnRadius);

                    // 3. Calculate potential position
                    Vector2 potentialPos = (Vector2)playerTransform.position + randomDirection * randomDistance;

                    // 4. Check if this position is INSIDE the map boundaries
                    if (potentialPos.x >= minMapLimit.x && potentialPos.x <= maxMapLimit.x &&
                        potentialPos.y >= minMapLimit.y && potentialPos.y <= maxMapLimit.y)
                    {
                        spawnPos = potentialPos;
                        validPositionFound = true;
                        break; // Found a good spot, stop looking
                    }
                }

                // Only spawn if we found a valid position inside the map
                if (validPositionFound)
                {
                    Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw Map Boundaries (Yellow Box)
        Gizmos.color = Color.yellow;
        Vector3 center = new Vector3((minMapLimit.x + maxMapLimit.x) / 2, (minMapLimit.y + maxMapLimit.y) / 2, 0);
        Vector3 size = new Vector3(maxMapLimit.x - minMapLimit.x, maxMapLimit.y - minMapLimit.y, 0);
        Gizmos.DrawWireCube(center, size);

        // Draw Spawn Radius (Red Circles around Player)
        if (playerTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, minSpawnRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerTransform.position, maxSpawnRadius);
        }
    }
}