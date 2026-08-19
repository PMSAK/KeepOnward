using UnityEngine;

public class Coins : Pickup
{
    [SerializeField] int addScore = 100;

    protected override void OnPickup()
    {
        Score score = FindAnyObjectByType<Score>();
        
        score.AddScore(addScore);
    }
}
