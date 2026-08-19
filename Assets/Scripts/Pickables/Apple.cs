using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] int increaseChunkSpeedVar = 3;

    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    protected override void OnPickup()
    {
        levelGenerator.ChangeChunkSpeed(increaseChunkSpeedVar);
    }
}
