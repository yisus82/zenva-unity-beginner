using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public int level = 1;
    public int maxLevel = 2;

    public static GameManager instance;

    private Text scoreText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseScore(int points)
    {
        score += points;

        UpdateScore();

        if (score > highScore)
        {
            highScore = score;
        }

    }

    public void RestartGame()
    {
        score = 0;
        level = 1;
        LoadLevel(level);
    }

    public void NextLevel()
    {
        if (level == maxLevel)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
            level++;
            LoadLevel(level);
        }

    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    public void SetScoreText(Text scoreText)
    {
        this.scoreText = scoreText;
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
