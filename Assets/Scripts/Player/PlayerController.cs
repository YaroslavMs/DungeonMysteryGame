using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public PlayerStats playerStats;
    public Animator _animator;
    public bool _facingRight = true;
    private Rigidbody2D _rigidbody2D;
    private State currentState;

    private void Start()
    {
        
        _facingRight = true;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        currentState = new StateIdle(this);
    }
    void Update()
    {
        currentState = currentState.Update();
    }
    private void FixedUpdate()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + currentState.FixedUpdate());
    }
    public void Flip()
    {
        _facingRight = !_facingRight;
        transform.localScale =
            new Vector3(transform.localScale.x * (-1), transform.localScale.y, transform.localScale.z);
    }
}