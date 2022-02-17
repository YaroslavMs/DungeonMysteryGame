using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private Animator _animator;
    private bool _facingRight = true;
    private float _savedAttackStartTime;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _direction;

    private void Start()
    {
        _facingRight = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
        switch (playerStats.currentState)
        {
            case PlayerStats.States.Idle:
                _direction = HandleInput();
                _animator.SetBool("Running", false);
                break;
            case PlayerStats.States.Walking:
                _direction = HandleInput();
                _animator.SetBool("Running", true);
                break;
            case PlayerStats.States.Attacking:
                _direction = Vector2.zero;
                Attack();
                break;
                
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _direction * playerStats.movementSpeed * Time.deltaTime);
    }

    private Vector2 HandleInput()
    {
        
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        playerStats.currentState = input == Vector2.zero
            ? PlayerStats.States.Idle
            : playerStats.currentState = PlayerStats.States.Walking;

        if (_facingRight && input.x < 0)
        {
            Flip();
        }
        else if (!_facingRight && input.x > 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerStats.currentState = PlayerStats.States.Attacking;
            _animator.SetTrigger("Attack");
            _savedAttackStartTime = Time.time;
        }
        return input.normalized;
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale =
            new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }

    private void Attack()
    {
        if (Time.time - _savedAttackStartTime > _animator.GetCurrentAnimatorStateInfo(0).length / 2)
        {
            playerStats.currentState = PlayerStats.States.Walking;
        }
    }
    
}