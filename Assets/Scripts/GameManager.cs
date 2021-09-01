using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int score = 0;

    public GameObject player;
    public Player playerScript;
    public GameObject targetPrefab;
    public GameObject bombPrefab;
    private int bombSpawnRate = 5;

    public GameObject exposionEffects;

    [SerializeField] MainUIScript uiScript;
    [SerializeField] int scoreMultiplicityToShow;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    public void StartGame()
    {
        DestroyAllItems();
        playerScript.fuel = playerScript.maxFuel;
        player.SetActive(true);
        score = 0;
        SpawnTarget();

        uiScript.ActivatePlaymodeUI();
    }

    public void GameOver()
    {
        playerScript.fuel = 0;
        playerScript.speed = Vector3.zero;
        playerScript.transform.rotation = Quaternion.identity;
        player.SetActive(false);
        Instantiate(exposionEffects, player.transform.position, exposionEffects.transform.rotation);

        uiScript.ActivateRestartScreen();
    }

    public void TargetCollected(GameObject target)
    {
        score++;
        playerScript.fuel++;
        Destroy(target);
        SpawnTarget();
        if (score % scoreMultiplicityToShow == 0)
            uiScript.ShowScore();
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
