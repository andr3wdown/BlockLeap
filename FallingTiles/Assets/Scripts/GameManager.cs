using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    float score = 0;
    float highScore = 0;
    public Text scoreText;
    public Text gmScoreText;
    public Text hScoreText;
    public bool isGameOver = false;
    float r = 1;
    public GameObject gameOverScreen;
    public GameObject controls;
    private void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
    }
    void Update ()
    {
        if (isGameOver)
        {
            controls.SetActive(false);
            r -= Time.deltaTime;
            Color c = Camera.main.backgroundColor;
            c.b = r;
            c.g = r;
            Camera.main.backgroundColor = c;
            score = Mathf.RoundToInt(score);
            if(score > highScore)
            {
                highScore = score;
            }
            if(r <= 0)
            {
                
                gmScoreText.text = "" + Mathf.RoundToInt(score);
                hScoreText.text = "" + Mathf.RoundToInt(highScore);
                gameOverScreen.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
           if(Time.timeScale < 1)
            {
                Time.timeScale = 1;
            }

            scoreText.text = "" + Mathf.RoundToInt(score);
        }
	}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(highScore));
    }
    public void AddScore()
    {
        score += Time.deltaTime;
    }
}
