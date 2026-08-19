using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] Transform chunkParent;
    [SerializeField] int numOfChunks = 12;
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f;
    [SerializeField] float chunkLength = 10f;

    [SerializeField] CameraController cameraController;

    Vector3 spacing = new Vector3(0f, 0f, 10f);
    List<GameObject> chunks = new List<GameObject>();

    void Start()
    {
        SpawnChunks();
    }

    void Update()
    {
        MoveChunks();
    }

    void SpawnChunks()
    {
        for (int i = 0; i < numOfChunks; i++)
        {
            GameObject newChunk = Instantiate(chunkPrefab, transform.position + (i*spacing), Quaternion.identity, chunkParent);
            chunks.Add(newChunk);
        }
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(-transform.forward * (moveSpeed * Time.deltaTime));

            if (chunk.transform.position.z < Camera.main.transform.position.z - chunkLength)
            {
                chunks.Remove(chunk);
                Destroy(chunk);

                Vector3 newSpawnPos = chunks[chunks.Count-1].transform.position + spacing;
                GameObject newChunk = Instantiate(chunkPrefab, newSpawnPos, Quaternion.identity, chunkParent);

                chunks.Add(newChunk);
            }
        }
    }

    public void ChangeChunkSpeed(int speedAmount)
    {
        float newMoveSpeed = moveSpeed + speedAmount;
        newMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (newMoveSpeed != moveSpeed)
        {
            moveSpeed = newMoveSpeed;

            //float newGravityZ = Physics.gravity.z - speedAmount;
            //newGravityZ = Mathf.Clamp(newGravityZ, minGravity, maxGravity);

            //Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, newGravityZ);

            cameraController.ChangeCameraFOV(speedAmount);
        }
    }
}
