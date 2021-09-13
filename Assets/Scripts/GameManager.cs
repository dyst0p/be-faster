using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    private Player _playerScript;
    
    [Header("Spawn")]
    [SerializeField]
    private GameObject _targetPrefab;
    [SerializeField]
    private GameObject _bombPrefab;
    [SerializeField]
    private int _bombSpawnRate;

    [Header("UI")]
    [SerializeField]
    private MainUIScript _uiScript;
    [SerializeField]
    private int _scoreMultiplicityToShow;
    
    private static bool s_musicOn;
    public int Score { get; private set; }
    public int BestScore { get; private set; }
    
    private PlayerData _data;



    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _playerScript = Player.Instance;
        LoadData();
        Debug.Log(s_musicOn);
        _playerScript.gameObject.SetActive(false);
    }

    // data flow

    void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            _data = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            _data = new PlayerData();
        }

        s_musicOn = _data.MusicOn;
        AudioSwitch(s_musicOn);

        BestScore = _data.BestScore;
    }

    public void SaveData()
    {
        _data.MusicOn = s_musicOn;
        _data.BestScore = BestScore;

        string json = JsonUtility.ToJson(_data);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }

    // buttons

    public void StartGame()
    {
        DestroyAllItems();
        _playerScript.FullTank();
        _playerScript.gameObject.SetActive(true);
        Score = 0;
        Spawn();

        _uiScript.ActivatePlaymodeUI();
    }

    public void ExitToMenu()
    {
        DestroyAllItems();
        _uiScript.ActivateMenuUI();
    }

    public void AudioSwitch()
    {
        s_musicOn = AudioListener.pause;
        AudioSwitch(s_musicOn);
    }

    public void AudioSwitch(bool toggle)
    {
        AudioListener.pause = !s_musicOn;
        _uiScript.SwitchMusicToggleSprite(s_musicOn);
    }
    // interactions

    public void GameOver()
    {
        _playerScript.Explosion();

        _uiScript.ActivateRestartScreen();

        if (Score > BestScore)
        {
            BestScore = Score;
            _uiScript.ShowNewRecord();
            SaveData();
        }
        else
        {
            _uiScript.ShowBestScore();
        }
    }

    public void AddScore()
    {
        Score++;
        if (Score % _scoreMultiplicityToShow == 0)
            _uiScript.ShowScore();
    }

    // spawn objects
    public void Spawn()
    {
        SpawnTarget();
        if (Score % _bombSpawnRate == 0)
            SpawnBomb();
    }
    
    private void SpawnTarget()
    {
        Instantiate(_targetPrefab, RandomPosition(), _targetPrefab.transform.rotation);
    }

    private void SpawnBomb()
    {
        Instantiate(_bombPrefab, RandomPosition(), _targetPrefab.transform.rotation);
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