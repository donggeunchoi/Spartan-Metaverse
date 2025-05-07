using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance ;

    [Header("계단")]
    [Space(10)]
    public GameObject[] Stairs;
    public bool[] isTurn;

    private enum State {Start, Left, Right};
    private State state;
    private Vector3 oldPosition;

    [Header("UI")]
    [Space(10)]
    public GameObject UI_GameOver;
    public GameObject UI_Start;
    public TextMeshProUGUI ShowScoreText;
    public TextMeshProUGUI MaxScoreText;
    public TextMeshProUGUI NowScoreText;
    private int maxScore = 0;
    private int nowScore = 0;

    void Awake()
    {
        UI_Start.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        InitStairs();
    }

    public void Init()
    {
        Instance = this;
        state = State.Start;
        oldPosition = Vector3.zero;

        isTurn = new bool[Stairs.Length];

        for(int i = 0; i < Stairs.Length; i++)
        {
            Stairs[i].transform.position = Vector3.zero;
            isTurn[i] = false;
        }

        nowScore = 0;
        ShowScoreText.text = nowScore.ToString();

        UI_GameOver.SetActive(false);
    }

    public void InitStairs()
    {
        for(int i = 0; i< Stairs.Length; i++)
        {
            switch(state)
            {
                case State.Start:
                    Stairs[i].transform.position = new Vector3(1.7f, -2.4f, 0);
                    state = State.Right;
                    break;
                case State.Left:
                    Stairs[i].transform.position = oldPosition + new Vector3(-1.7f, 2f, 0);
                    isTurn[i] = true;
                    break;
                case State.Right:
                    Stairs[i].transform.position = oldPosition + new Vector3(1.7f, 2f, 0);
                    isTurn[i] = false;
                    break;
            }
            oldPosition = Stairs[i].transform.position;

            if(i != 0 )
            {
                int ran = Random.Range(0,5);

                if(ran < 2 && i < Stairs.Length - 1)
                {
                    state = state == State.Left? State.Right : State.Left;
                }
            }
        }
    }

    public void SpawnStair(int count)
    {
        int ran = Random.Range(0,5);

        if(ran < 2)
        {
            state = state == State.Left? State.Right : State.Left;
        }

        switch(state)
        {
            case State.Left:
                Stairs[count].transform.position = oldPosition + new Vector3(-1.7f, 2f, 0);
                isTurn[count] = true;
                break;
            case State.Right:
                Stairs[count].transform.position = oldPosition + new Vector3(1.7f, 2f, 0);
                isTurn[count] = false;
                break;
        }

        oldPosition = Stairs[count].transform.position;

    }

    public void StartGame()
    {
        UI_Start.SetActive(false);
        Time.timeScale = 1f;
        Init();
        InitStairs();
    }

    public void GameOver()
    {
        StartCoroutine(ShowGameOver());
    }

    IEnumerator ShowGameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        UI_GameOver.SetActive(true);

        if(nowScore > maxScore)
        {
            maxScore = nowScore;
        }
        MaxScoreText.text = maxScore.ToString();
        NowScoreText.text = nowScore.ToString();
    }

    public void AddScore()
    {
        nowScore++;
        ShowScoreText.text = nowScore.ToString();
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MadeSparta"); 
    }
}

