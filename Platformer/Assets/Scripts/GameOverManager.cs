using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text score;
    public Text highScore;

    private void Start()
    {
        score.text = "" + GameManager.instance.score;
        highScore.text = "" + GameManager.instance.highScore;
    }

    public void PlayAgain()
    {
        GameManager.instance.RestartGame();
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
