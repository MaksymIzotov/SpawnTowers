using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    [SerializeField] private EnemyData data;
    private float currentHp;

    private float multiplier;

    private void Start()
    {
        currentHp = data.hp * multiplier;
    }

    public void GetMultiplier(float mult)
    {
        multiplier = mult;
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
            Die();
    }

    public void Die()
    {
        MoneyController.Instance.AddAmount(data.moneyPerKill);

        Destroy(gameObject);
    }
}
