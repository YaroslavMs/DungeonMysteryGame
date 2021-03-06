using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : BaseEnemy
{
    public bool facingRight;
    public float hp;
    public float movementSpeed;
    public float runningMovementSpeed;
    public float timeToChangeDirection;
    public EnemyState CurrentState;
    public Animator animator;
    public GameObject player;
    public Bar healthBar;
    public int killedMoney;
    public int killedExp;
    public LayerMask wallLayer;

    public delegate void IsKilled(int money, int exp);

    public static event IsKilled Killed;


    private void Awake()
    {
        wallLayer = LayerMask.GetMask("Wall");
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        healthBar.SetMaxValueOnBar(hp);
        facingRight = true;
        CurrentState = new EnemyIdle(this);
    }

    void Update()
    {
        CurrentState.Update();
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        healthBar.SetValueOnBar(hp);
        if (hp <= 0)
        {
            Killed?.Invoke(killedMoney, killedExp);
            Destroy(gameObject);
        }
    }

    public void OnChildTriggerEnter(GameObject _player)
    {
        player = _player;
        CurrentState = CurrentState.ChangeState(new EnemyChase(this));
    }

    public void OnChildTriggerExit()
    {
        player = null;
        CurrentState = CurrentState.ChangeState(new EnemyIdle(this));
    }
}