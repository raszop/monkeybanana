using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    [SerializeField]
    private GameController gameController;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private List<Image> hearts = new List<Image>();
    [SerializeField]
    private GameObject gameOverScreen;
    [SerializeField]
    private Text gameOverScoreText;
    [SerializeField]
    private Text highestScoreText;

    public void RestartGame()
    {
        gameController.StartNewGame();
    }

    public void RefreshScoreUI(int scoreValue)
    {
        scoreText.text = scoreValue.ToString();
    }

    public void ReduceHeartCount(int heartIndex)
    {
        hearts[heartIndex].enabled = false;
    }

    public void ShowGameOverScreen(int scoreValue)
    {
        gameOverScreen.SetActive(true);
        UpdateHighestScore(scoreValue);
        SetGameOverScore(scoreValue);
    }
    
    private void SetGameOverScore(int scoreValue)
    {
        gameOverScoreText.text = "Your score:\n" + scoreValue.ToString();
        highestScoreText.text = "Highest score:\n" + Utilities.GetHighScore();
    }

    private void UpdateHighestScore(int scoreValue)
    {
        if(scoreValue > Utilities.GetHighScore())
        {
            Utilities.SetNewHighScore(scoreValue);
        }
    }

    public void SetupLivesUI()
    {
        foreach(Image heart in hearts)
        {
            heart.enabled = true;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale != 0 ? 0 : 1;
    }
}
