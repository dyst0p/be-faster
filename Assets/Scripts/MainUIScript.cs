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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePlaymodeUI()
    {
        startScreen.SetActive(false);
        restartScreen.SetActive(false);
        scoreText.SetToZero();
    }

    public void ActivateRestartScreen()
    {
        restartScreen.SetActive(true);
        scoreTextGameOver.text = $"Score: {gameManager.score}";
    }

    public void ShowScore() => scoreText.ShowScore(gameManager.score);
}
