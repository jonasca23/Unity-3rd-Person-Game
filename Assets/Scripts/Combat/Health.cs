using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int health;
    bool isInvunerable = false;

    public event Action OnTakeDamage;
    public event Action OnDie;

    public bool IsDead => health <= 0;

    private void Start()
    {
        health = maxHealth;
    }

    public void SetInvunerable(bool _isInvunerable)
    {
        isInvunerable = _isInvunerable;
    }

    public void DealDamage(int damage)
    {
        if (health <= 0) return;
        if (isInvunerable) return;

        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();

        if(health <= 0)
        {
            OnDie?.Invoke();
        }

        Debug.Log(health);
    }
}