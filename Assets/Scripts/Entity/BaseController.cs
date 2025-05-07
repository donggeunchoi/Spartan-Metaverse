using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection{get  {return movementDirection; }}

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection{get {return lookDirection; }}
      
    protected AnimationController animationController;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationController = GetComponent<AnimationController>();
    } 
    // Start is called before the first frame update
    protected virtual void Start()
    {
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
    }

    protected virtual void HandleAction()
    {
    
    }

    private void Movment(Vector2 direction)
    {
        direction = direction * 5;

        _rigidbody.velocity = direction;

        if (animationController != null)
        {
            animationController.Move(direction);
        }
        else
        {
            Debug.LogWarning("AnimationController가 할당되지 않았습니다.");
        }

    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }
}
