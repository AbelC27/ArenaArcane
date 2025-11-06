using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform playerTransform;
    public float spawnRadius = 15f;
    public float spawnInterval = 1.0f; 

    [Header("Difficulty Scaling")]
    public float timeBetweenDifficultyIncrease = 30f;
    public float spawnIntervalDecrement = 0.05f;
    public float minSpawnInterval = 0.2f;
    private float nextDifficultyIncreaseTime;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        nextDifficultyIncreaseTime = timeBetweenDifficultyIncrease;
    }

    void Update()
    {
        if (UpgradeManager.Instance == null)
        {
            return;
        }

        float currentTime = UpgradeManager.Instance.survivalTime;

        if (currentTime > nextDifficultyIncreaseTime)
        {
           spawnInterval -= spawnIntervalDecrement;
            if (spawnInterval < minSpawnInterval)
            {
                spawnInterval = minSpawnInterval;
            }
            nextDifficultyIncreaseTime += timeBetweenDifficultyIncrease;
            UnityEngine.Debug.Log($"DIFICULTATEA A CRESCUT! Noul interval de spawn: {spawnInterval}");
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
            Vector2 spawnPos = (Vector2)playerTransform.position + randomDirection * spawnRadius;
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}