using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance{get; private set;}

    [Header("EndUI")]
    public TextMeshProUGUI score;
    public TextMeshProUGUI BestScore;
    public TextMeshProUGUI combo;
    public TextMeshProUGUI BestCombo;

    [Header("GameUI")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI ComboText;
    

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(score == null)
        {
            score = GameObject.Find("score").GetComponent<TextMeshProUGUI>();
        }
        if(BestScore == null)
        {
            BestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
        }
        if(combo == null)
        {
            combo = GameObject.Find("combo").GetComponent<TextMeshProUGUI>();
        }
        if(BestCombo == null)
        {
            BestCombo = GameObject.Find("BestCombo").GetComponent<TextMeshProUGUI>();
        }
        if(ScoreText == null)
        {
            ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }
        if(ComboText == null)
        {
            ComboText = GameObject.Find("ComboText").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateScore(int scorecount)
    {
        score.text = scorecount.ToString();
        ScoreText.text = scorecount.ToString();
    }
    public void UpdateBestScore(int scorecount)
    {
        BestScore.text = scorecount.ToString();
    }
    public void UpdateCombo(int combocount)
    {
        combo.text = combocount.ToString();
        ComboText.text = combocount.ToString();
    }
    public void UpdateBestCombo(int combocount)
    {
        BestCombo.text = combocount.ToString();
    }

    public void ShowCanvas()
    {
        gameObject.SetActive(true);
        UpdateScore(ScoreManager.Instance.CurrentScore);
        UpdateBestScore(ScoreManager.Instance.BestScore);
        UpdateCombo(ScoreManager.Instance.CurrentCombo);
        UpdateBestCombo(ScoreManager.Instance.BestCombo);
    }
}
