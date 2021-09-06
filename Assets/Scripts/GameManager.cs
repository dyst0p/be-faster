using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int Score { get; private set; }

    [SerializeField] GameObject player;
    [SerializeField] Player playerScript;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] GameObject bombPrefab;
    private int bombSpawnRate = 5;

    [SerializeField] GameObject exposionEffects;

    [SerializeField] MainUIScript uiScript;
    [SerializeField] int scoreMultiplicityToShow;

    [SerializeField] static bool musicOn;
    public int BestScore { get; private set; }
    PlayerData data;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
        LoadData();
        Debug.Log(musicOn);
    }

    // data flow

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            data = new PlayerData();
        }

        musicOn = data.musicOn;
        AudioSwitch(musicOn);

        BestScore = data.bestScore;
    }

    public void SaveData()
    {
        data.musicOn = musicOn;
        data.bestScore = BestScore;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }

    // buttons

    public void StartGame()
    {
        DestroyAllItems();
        playerScript.FullTank();
        player.SetActive(true);
        Score = 0;
        SpawnTarget();

        uiScript.ActivatePlaymodeUI();
    }

    public void ExitToMenu()
    {
        DestroyAllItems();
        uiScript.ActivateMenuUI();
    }

    public void AudioSwitch()
    {
        musicOn = AudioListener.pause;
        AudioSwitch(musicOn);
    }

    public void AudioSwitch(bool toggle)
    {
        AudioListener.pause = !musicOn;
        uiScript.musicToggle.SwitchSprite(musicOn);
    }
    // interactions

    public void GameOver()
    {
        playerScript.speed = Vector3.zero;
        playerScript.transform.rotation = Quaternion.identity;
        player.SetActive(false);
        Instantiate(exposionEffects, player.transform.position, exposionEffects.transform.rotation);

        uiScript.ActivateRestartScreen();

        if (Score > BestScore)
        {
            BestScore = Score;
            uiScript.ShowNewRecord();
            SaveData();
        }
        else
        {
            uiScript.ShowBestScore();
        }
    }

    public void TargetCollected(GameObject target)
    {
        Score++;
        playerScript.RefillFuel();
        Destroy(target);
        SpawnTarget();
        if (Score % scoreMultiplicityToShow == 0)
            uiScript.ShowScore();
    }

    public void BombCollide()
    {
        GameOver();
    }

    // spawn and despawn objects
    private void SpawnTarget()
    {
        Instantiate(targetPrefab, RandomPosition(), targetPrefab.transform.rotation);

        //  this should be relocated
        if (Score % bombSpawnRate == 0)
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

[System.Serializable]
class PlayerData
{
    public bool musicOn = true;
    public int bestScore = 0;
    
}