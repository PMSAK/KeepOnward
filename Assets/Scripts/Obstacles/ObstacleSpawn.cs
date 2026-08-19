using System.Collections;
using UnityEngine;

public class ObstacleSpawn : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float obstacleSpawnDealyLessScore = 1.5f;
    [SerializeField] float obstacleSpawnDealyHighScore = 1f;
    [SerializeField] float obstacleSpawnDealyVeryHighScore = 0.5f;
    [SerializeField] int firstSpeedSwitchScoreValue = 10000; 
    [SerializeField] int secondSpeedSwitchScoreValue = 15000; 
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnRange = 3f;

    Score score;

    void Awake()
    {
        score = FindAnyObjectByType<Score>();
    }

    void Start()
    {
        StartCoroutine(ObstacleSpawnCoroutine());
    }

    IEnumerator ObstacleSpawnCoroutine()
    {
        while (true)
        {
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), transform.position.y, transform.position.z);
            
            if (score.CurrentScore <= firstSpeedSwitchScoreValue)
            {
                yield return new WaitForSeconds(obstacleSpawnDealyLessScore);
            }

            else if (score.CurrentScore > firstSpeedSwitchScoreValue && score.CurrentScore <= secondSpeedSwitchScoreValue)
            {
                yield return new WaitForSeconds(obstacleSpawnDealyHighScore);
            }

            else if (score.CurrentScore > secondSpeedSwitchScoreValue)
            {
                yield return new WaitForSeconds(obstacleSpawnDealyVeryHighScore);
            }

            Instantiate(obstacle, spawnPos, Random.rotation, obstacleParent);
        }
    }
}
