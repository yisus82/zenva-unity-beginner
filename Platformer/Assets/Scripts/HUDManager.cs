using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Text scoreText;

    private void Start()
    {
        GameManager.instance.SetScoreText(scoreText);
        GameManager.instance.UpdateScore();
    }
}
