using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public UnityEvent onLose;

    [SerializeField] private float maxHP;
    private float currentHP;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void Die()
    {
        Time.timeScale = 0;
        onLose.Invoke();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
            Die();
    }
}
