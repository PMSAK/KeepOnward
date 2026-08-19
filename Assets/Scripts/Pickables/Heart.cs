using UnityEngine;

public class Heart : Pickup
{
    Lives lives;

    void Start()
    {
        lives = FindAnyObjectByType<Lives>();
    }

    protected override void OnPickup()
    {
        lives.GainLife();
    }
}
