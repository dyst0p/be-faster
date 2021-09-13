using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIScript : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField]
    private GameObject _startScreen;
    [SerializeField]
    private GameObject _restartScreen;
    [SerializeField]
    private MusicToggle _musicToggle;
    [SerializeField]
    private ScoreText _scoreText;
    [SerializeField]
    private Text _scoreTextGameOver;
    [SerializeField]
    private Text _bestScoreText;

    private GameManager _gameManager;

    public void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void ActivatePlaymodeUI()
    {
        _startScreen.SetActive(false);
        _restartScreen.SetActive(false);
        _scoreText.SetToZero();
    }

    public void ActivateMenuUI()
    {
        _startScreen.SetActive(true);
        _restartScreen.SetActive(false);
    }

    public void ActivateRestartScreen()
    {
        _restartScreen.SetActive(true);
        _scoreTextGameOver.text = $"Score: {_gameManager.Score}";
    }

    public void ShowScore() => _scoreText.ShowScore(_gameManager.Score);

    public void ShowBestScore()
    {
        _bestScoreText.text = $"Best Score: {_gameManager.BestScore}";
    }

    public void ShowNewRecord()
    {
        _bestScoreText.text = "New Record!";
    }

    public void SwitchMusicToggleSprite(bool toggle) => _musicToggle.SwitchSprite(toggle);
}
