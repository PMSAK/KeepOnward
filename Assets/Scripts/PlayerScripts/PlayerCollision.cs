using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int slowChunkDownVar = -5;
    [SerializeField] float maxCoolDownTime = 1f;
    [SerializeField] int subtractScore = -50;

    float coolDownTimer;
    LevelGenerator levelGenerator;
    Score score;
    [SerializeField] Lives lives;

    void Start()
    {
        levelGenerator = FindAnyObjectByType<LevelGenerator>();
        score = FindAnyObjectByType<Score>();
    }

    void Update()
    {
        coolDownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (coolDownTimer < maxCoolDownTime)
        {
            return;
        }

        anim.SetTrigger("Hit");

        levelGenerator.ChangeChunkSpeed(slowChunkDownVar);

        score.AddScore(subtractScore);

        lives.LoseLife();

        coolDownTimer = 0f;
    }
}
