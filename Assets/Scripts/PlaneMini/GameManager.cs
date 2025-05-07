using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance => gameManager;

    [Header("Canvas")]
    public GameObject gameoverCanvas;
    public GameObject startMenu;
    
    private int currentScore = 0;
    
    private void Awake()
    {

        if(gameManager == null)
        {
            gameManager = this; 
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Start()
    {
        Time.timeScale = 0f;
        startMenu.SetActive(true);
        ScoreManager.Instance.ResetScore();
    }

    public void OnStartButtonPressed()
    {
        Time.timeScale = 1f;
        startMenu.SetActive(false);
    }

    public void AddScore(int score)
    {
        ScoreManager.Instance.AddScore(score);    
        Debug.Log("Score: " + currentScore);
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");
        
        if(gameoverCanvas != null)
        {
            gameoverCanvas.SetActive(true);
            UIManager.Instance.ShowCanvas();
        }
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Start();
        gameoverCanvas.SetActive(false);
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MadeSparta"); 
    }
    
}