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
    [SerializeField] 
    private Text _versionText;

    
    

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

    public void EasterEgg()
    {
        _versionText.text = "Eastern Egg mode";
        _versionText.color = Color.green;
    }

    public void ShowEasterEgg()
    {
        StartCoroutine(ColorfulText());
    }

    IEnumerator ColorfulText()
    {
        _gameOverText.color = Color.green;
        _gameOverText.text = "Happy Birthday Peter!";
        _gameOverText.gameObject.SetActive(true);
        while (true)
        {
            Color32.LerpUnclamped(Color.green, Color.yellow, Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
