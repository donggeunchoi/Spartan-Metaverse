using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizzard : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Vector3 startPos;
    private Vector3 oldpos;
    private bool isTurn = false;

    private int moveCount = 0;
    private int turnCount = 0;
    private int spawnCount = 0;

    private bool isDead = false;

    private GameManager2 gameManager2;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position;
        
        Init();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            CharTurn();
        }
        else if(Input.GetMouseButtonDown(0))
        {
            CharMove();
        }
    }

    private void Init()
    {
        anim.SetBool("isDead", false);
       transform.position = startPos;
        oldpos = startPos;
        moveCount = 0;
        turnCount = 0;
        spawnCount = 0;
        isTurn = false;
        spriteRenderer.flipX = isTurn;
        isDead = false;
    }

    private void CharTurn()
    {
        isTurn = isTurn == true? false : true;

        spriteRenderer.flipX = isTurn;
    }

    private void CharMove()
    {
        if(isDead)
        {
            return;
        }

        moveCount++;
        MoveDirection();

        if(ifFailTurn())
        {
            Time.timeScale = 0;
            GameManager2.Instance.GameOver();
            
            Debug.Log("턴 실패");
            return;
        }

        if(moveCount > 5)
        {
            RespawnStair();
        }

        GameManager2.Instance.AddScore();
    }

    private void MoveDirection()
    {
        if(isTurn)
        {
            oldpos += new Vector3(-1.6f,2f,0);
        }
        else
        {
            oldpos += new Vector3(1.6f,2f,0);
        }

        transform.position = oldpos;
        anim.SetTrigger("Move");
    }
    private bool ifFailTurn()
    {
        bool result = false;

        if(GameManager2.Instance.isTurn[turnCount] != isTurn)
        {
            return true;
        }

        turnCount++;

        if(turnCount > GameManager2.Instance.isTurn.Length - 1)
        {
            turnCount = 0;
        }

        return result;
    }

    private void RespawnStair()
    {
        GameManager2.Instance.SpawnStair(spawnCount);
        spawnCount++;

        if(spawnCount > GameManager2.Instance.Stairs.Length - 1)
        {
            spawnCount = 0;
        }
    }

    public void ButtonReStart()
    {
        Init();
        GameManager2.Instance.Init();
        GameManager2.Instance.InitStairs();
    }

    public void ButtonStart()
    {
        GameManager2.Instance.StartGame();
    }
}
