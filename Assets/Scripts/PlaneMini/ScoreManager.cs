using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int CurrentScore{get; private set;}
    public int CurrentCombo{get; private set;}
    public int BestScore{get; private set;}
    public int BestCombo{get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadBestScores();

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator Start()
    {
        // UIManager가 로드될 때까지 기다림
        while (UIManager.Instance == null)
        {
            yield return null;
        }
        UIManager.Instance.UpdateScore(CurrentScore);
        UIManager.Instance.UpdateCombo(CurrentCombo);
        UIManager.Instance.UpdateBestScore(BestScore);
        UIManager.Instance.UpdateBestCombo(BestCombo);
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        CurrentCombo = 0;
        UIManager.Instance.UpdateScore(0);
        UIManager.Instance.UpdateCombo(0);  
    }

    public void AddScore(int amount)
    {
        CurrentScore += amount;

        if(UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScore(CurrentScore);
        }
        
        if(CurrentScore > BestScore)
        {
            BestScore = CurrentScore;
            if(UIManager.Instance != null)
            {
                UIManager.Instance.UpdateBestScore(BestScore);
            }
            
            SaveBestScores();
        }

        AddCombo();
    }

    public void AddCombo()
    {
        if(CurrentScore >= 5)
        {
            CurrentCombo++;
            if(UIManager.Instance != null)
            {
                UIManager.Instance.UpdateCombo(CurrentCombo);
            }
            

            if(CurrentCombo > BestCombo)
            {
                BestCombo = CurrentCombo;
                if(UIManager.Instance != null)
                {
                    UIManager.Instance.UpdateBestCombo(BestCombo);
                }
                
                SaveBestScores();
            }
        }
        
        
    }

    

    private void SaveBestScores()
    {
        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("BestCombo", BestCombo);
        PlayerPrefs.Save();
    }

    private void LoadBestScores()
    {
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        BestCombo = PlayerPrefs.GetInt("BestCombo", 0);
    }
}
