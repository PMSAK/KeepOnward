using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    [SerializeField] Image[] hearts;
    [SerializeField] int currentLives = 5;
    [SerializeField] int maxLives = 5;

    [SerializeField] GameObject gameOverPanel;

    public void LoseLife()
    {
        if (currentLives <= 0)
        {
            return;
        }

        currentLives--;

        hearts[currentLives].enabled = false;

        if (currentLives <= 0)
        {
            Invoke(nameof(GameOver), 1f);
        }
    }

    public void GainLife()
    {
        if (currentLives >= maxLives)
        {
            return;
        }

        hearts[currentLives].enabled = true;
        currentLives++;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);

        GameOverMenu gameOverMenu = FindAnyObjectByType<GameOverMenu>();

        if (gameOverMenu != null)
        {
            gameOverMenu.ShowScore();
        }

        Time.timeScale = 0f;
    }
}
