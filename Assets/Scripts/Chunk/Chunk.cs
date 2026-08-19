using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject heartPrefab;
    [SerializeField] float fenceOffset = 0.5f;
    [SerializeField] float appleOffset = 0.5f;
    [SerializeField] float coinOffset = 0.5f;
    [SerializeField] float heartOffset = 0.5f;

    [SerializeField] float appleSpawnChance = 0.5f;
    [SerializeField] float coinSpawnChance = 0.7f;
    [SerializeField] float heartSpawnChance = 0.3f;
    [SerializeField] float coinSeparationLength = 2f;

    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    List<int> availableLanes = new List<int> {0,1,2};

    void Start()
    {
        SpawnFence();
        SpawnApple();
        SpawnCoin();
        SpawnHeart();
    }

    private void SpawnFence()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0)
            {
                break;
            }

            int laneIdx = SelectLane();

            Vector3 spawnPos = new Vector3(lanes[laneIdx], transform.position.y - fenceOffset, transform.position.z);
            Instantiate(fencePrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0)
        {
            return;
        }

        int laneIdx = SelectLane();

        Vector3 spawnPos = new Vector3(lanes[laneIdx], transform.position.y + appleOffset, transform.position.z);
        Instantiate(applePrefab, spawnPos, Quaternion.identity, this.transform);
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0)
        {
            return;
        }

        int coinsToSpawn = Random.Range(1,6);

        int laneIdx = SelectLane();

        float topOfChunkZ = transform.position.z + (coinSeparationLength * 2f);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPosZ = topOfChunkZ - (coinSeparationLength * i);
            Vector3 spawnPos = new Vector3(lanes[laneIdx], transform.position.y + coinOffset, spawnPosZ);

            Instantiate(coinPrefab, spawnPos, Quaternion.identity, this.transform);
        }
    }

    void SpawnHeart()
    {
        if (Random.value > heartSpawnChance || availableLanes.Count <= 0)
        {
            return;
        }

        int laneIdx = SelectLane();

        Vector3 spawnPos = new Vector3(lanes[laneIdx], transform.position.y + heartOffset, transform.position.z);
        Instantiate(heartPrefab, spawnPos, Quaternion.identity, this.transform);
    }

    private int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);
        return selectedLane;
    }
}
