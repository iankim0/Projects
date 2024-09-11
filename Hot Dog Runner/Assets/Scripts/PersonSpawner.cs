using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{

    private float _currentTimer;
    private float _nextSpawnTime;
    public GameObject[] people;  // Array to hold the 6 people
    public float minSpawnTime;
    public float maxSpawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetNextSpawnTime();  // Set the initial spawn time
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += Time.deltaTime;
        if (_currentTimer >= _nextSpawnTime)
        {
            SpawnNewObstacles();
            _currentTimer = 0;
            SetNextSpawnTime();  // Set a new spawn time
        }
    }

    private void SpawnNewObstacles()
    {
        int randomIndex = Random.Range(0, people.Length);
        GameObject personToSpawn = people[randomIndex];
        Vector3 spawnPositionBottomObstacle = transform.position;
        spawnPositionBottomObstacle.y = -2.54f;  // Set y to -2.54
        spawnPositionBottomObstacle.z = 0;       // Keep z at 0
        Instantiate(personToSpawn, spawnPositionBottomObstacle, Quaternion.identity);
    
    }

    private void SetNextSpawnTime()
    {
        _nextSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}

