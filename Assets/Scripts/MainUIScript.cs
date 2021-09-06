using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIScript : MonoBehaviour
{
    [SerializeField] GameManager gameManager; // just in case

    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject restartScreen;
    [SerializeField] Text scoreTextGameOver;
    [SerializeField] ScoreText scoreText;
    [SerializeField] Text bestScoreText;

    public MusicToggle musicToggle;

    public void ActivatePlaymodeUI()
    {
        startScreen.SetActive(false);
        restartScreen.SetActive(false);
        scoreText.SetToZero();
    }

    public void ActivateMenuUI()
    {
        startScreen.SetActive(true);
        restartScreen.SetActive(false);
    }

    public void ActivateRestartScreen()
    {
        restartScreen.SetActive(true);
        scoreTextGameOver.text = $"Score: {gameManager.Score}";
    }

    public void ShowScore() => scoreText.ShowScore(gameManager.Score);

    public void ShowBestScore()
    {
        bestScoreText.text = $"Best Score: {gameManager.BestScore}";
    }

    public void ShowNewRecord()
    {
        bestScoreText.text = "New Record!";
    }
}
