using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField]
    private float _flashDuration;
    
    private Text _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<Text>();
    }

    public void ShowScore(int score)
    {
        _scoreText.enabled = true;
        _scoreText.text = score.ToString();
        Invoke("TurnOff", _flashDuration);
    }

    private void TurnOff()
    {
        _scoreText.enabled = false;
    }

    public void SetToZero()
    {
        _scoreText.text = "";
    }
}
