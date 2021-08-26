using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public GameObject player;
    public GameObject targetPrefab;
    public GameObject bombPrefab;
    private int bombSpawnRate = 5;

    public GameObject exposionEffects;

    public GameObject restartScreen;
    public Text scoreTextGameOver;
    public ScoreText scoreText;
    public int scoreMultiplicityToShow;

    private bool isGameOver = true;

    private void Start()
    {
        restartScreen.SetActive(false);
    }

    public void StartGame()
    {
        DestroyAllItems();
        isGameOver = false;
        player.GetComponent<Player>().fuel = player.GetComponent<Player>().maxFuel;
        player.SetActive(true);
        score = 0;
        scoreText.SetToZero();
        SpawnTarget();
    }

    public void GameOver()
    {
        isGameOver = true;
        player.GetComponent<Player>().fuel = 0;
        player.GetComponent<Player>().speed = Vector3.zero;
        player.GetComponent<Player>().transform.rotation = Quaternion.identity;
        player.SetActive(false);
        Instantiate(exposionEffects, player.transform.position, exposionEffects.transform.rotation);

        restartScreen.SetActive(true);
        scoreTextGameOver.text = $"Score: {score}";
    }

    public void TargetCollected(GameObject target)
    {
        score++;
        player.GetComponent<Player>().fuel++;
        Destroy(target);
        SpawnTarget();
        if (score % scoreMultiplicityToShow == 0)
            scoreText.ShowScore(score);
    }

    public void BombCollide()
    {
        GameOver();
    }

    private void SpawnTarget()
    {
        Vector2 spawnPos = RandomPosition();
        while (Physics2D.CircleCast(spawnPos, 1.5f, Vector2.zero, 0.0f).collider != null)
        {
            spawnPos = RandomPosition();
        }
        Instantiate(targetPrefab, spawnPos, targetPrefab.transform.rotation);

        //  this should be relocated
        if (score % bombSpawnRate == 0)
            SpawnBomb();
    }

    private void SpawnBomb()
    {
        Vector2 spawnPos = RandomPosition();
        while (Physics2D.CircleCast(spawnPos, 1.5f, Vector2.zero, 0.0f).collider != null)
        {
            spawnPos = RandomPosition();
        }
        Instantiate(bombPrefab, spawnPos, targetPrefab.transform.rotation);
    }

    private Vector2 RandomPosition()
    {
        float targetXPos = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        float targetYPos = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        return new Vector2(targetXPos, targetYPos);
    }

    private void DestroyAllItems()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Items"))
            Destroy(item);
    }
}
