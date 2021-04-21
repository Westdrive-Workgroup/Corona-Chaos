using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    [SerializeField] 
    private SpawnManager _spawnManager;
    

    [SerializeField] 
    private UIManager _uiManager;


    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    
    public void GameOver()
    {
        if(_spawnManager != null)
            _spawnManager.onPlayerDeath();
        if(_uiManager != null)
            _uiManager.ShowGameOver();
    }

    public void UpdateHealth(int lives)
    {
        _uiManager.UpdateHealth(lives);
    }


    public void AddScore(int score)
    {
        _uiManager.AddScore(score);
    }
}
