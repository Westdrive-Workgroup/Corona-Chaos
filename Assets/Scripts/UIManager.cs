using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int _score = 0;
    [SerializeField] 
    private Text _scoreText;
    [SerializeField] 
    private Text _healthText;
    [SerializeField] 
    private Text _gameOverText;
    
    void OnEnbale()
    {
        if (_scoreText == null)
            Debug.LogError("Score Text is missing!");
        if (_healthText == null)
            Debug.LogError("Health Text is missing!");
        if (_gameOverText == null)
            Debug.LogError("Game over text is missing!");
    }
    public int GetScore()
    {
        return _score;
    }
    void Start()
    {
        _gameOverText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + _score;
    }

    public void ShowGameOver()
    {
        _gameOverText.gameObject.SetActive(true);
    }
    public void UpdateHealth(int health)
    {
        Color healthColor = Color32.Lerp(Color.red,Color.green , Mathf.Clamp01(health));
        _healthText.color = healthColor;
        _healthText.text = " lives: " + health;
    }
    public void AddScore(int score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
    }

    

    

    
}
