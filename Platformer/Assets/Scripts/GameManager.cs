using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int highScore = 0;
    public int level = 1;
    public int maxLevel = 2;

    public static GameManager instance;

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

        print("New score: " + score);

        if (score > highScore)
        {
            highScore = score;
            print("New high score: " + highScore);
        }

    }

    public void ResetGame()
    {
        score = 0;
        level = 1;
        LoadLevel(level);
    }

    public void NextLevel()
    {
        if (level == maxLevel)
        {
            Time.timeScale = 0;
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
}
