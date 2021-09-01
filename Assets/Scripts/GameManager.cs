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
        playerScript.FullTank();
        player.SetActive(true);
        score = 0;
        SpawnTarget();

        uiScript.ActivatePlaymodeUI();
    }

    public void ExitToMenu()
    {
        DestroyAllItems();
        uiScript.ActivateMenuUI();
    }

    public void GameOver()
    {
        //playerScript.fuel = 0;
        playerScript.speed = Vector3.zero;
        playerScript.transform.rotation = Quaternion.identity;
        player.SetActive(false);
        Instantiate(exposionEffects, player.transform.position, exposionEffects.transform.rotation);

        uiScript.ActivateRestartScreen();
    }

    public void TargetCollected(GameObject target)
    {
        score++;
        playerScript.RefillFuel();
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
        Instantiate(targetPrefab, RandomPosition(), targetPrefab.transform.rotation);

        //  this should be relocated
        if (score % bombSpawnRate == 0)
            SpawnBomb();
    }

    private void SpawnBomb()
    {
        Instantiate(bombPrefab, RandomPosition(), targetPrefab.transform.rotation);
    }

    private Vector2 RandomPosition()
    {
        Vector2 spawnPos = new Vector2();
        do
        {
            spawnPos.x = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
            spawnPos.x = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        }
        while (Physics2D.CircleCast(spawnPos, 1.5f, Vector2.zero, 0.0f).collider != null);

        return spawnPos;
    }

    private void DestroyAllItems()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Items"))
            Destroy(item);
    }
}
