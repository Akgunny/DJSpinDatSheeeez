using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawning")]
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f;
    public float spawnX = 10f;
    public float yMin = -3.5f;
    public float yMax = 3.5f;

    private float timer;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        if (obstaclePrefabs == null || obstaclePrefabs.Length == 0) return;

        GameObject prefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        float y = Random.Range(yMin, yMax);
        Instantiate(prefab, new Vector3(spawnX, y, 0f), Quaternion.identity);
    }
}
