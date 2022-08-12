using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoneyController : MonoBehaviour, IValuable
{
    #region Singleton Init
    public static MoneyController Instance;

    private void Awake()
    {
        Instance = this;
    }
#endregion

    public UnityEvent onValueChanged;

    private const string saveName = "Money";
    private int money;

    private void Start()
    {
        if (PlayerPrefs.HasKey(saveName))
            SetAmount(PlayerPrefs.GetInt(saveName));
        else
            SetAmount(0);
    }

    #region Interface Implementation
    public void AddAmount(int amount)
    {
        money += amount;
        PlayerPrefs.SetInt(saveName, money);

        onValueChanged.Invoke();
    }

    public int GetAmount()
    {
        return money;
    }

    public void SetAmount(int amount)
    {
        money = amount;
        PlayerPrefs.SetInt(saveName, money);

        onValueChanged.Invoke();
    }

    #endregion
}
