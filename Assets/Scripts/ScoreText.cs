using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] float flashDuration;
    
    private Text scoreText;

    private void Start()
    {
        scoreText = GetComponent<Text>();
    }

    public void ShowScore(int score)
    {
        scoreText.enabled = true;
        scoreText.text = score.ToString();
        Invoke("TurnOff", flashDuration);
    }

    private void TurnOff()
    {
        scoreText.enabled = false;
    }

    public void SetToZero()
    {
        scoreText.text = "";
    }
}
