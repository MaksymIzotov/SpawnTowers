using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TowerPriceController : MonoBehaviour, IValuable
{
    public UnityEvent onValueChanged;

    private const string saveName = "TowerPrice";
    private int price;

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
        price += amount;
        PlayerPrefs.SetInt(saveName, price);

        onValueChanged.Invoke();
    }

    public int GetAmount()
    {
        return price;
    }

    public void SetAmount(int amount)
    {
        price = amount;
        PlayerPrefs.SetInt(saveName, price);

        onValueChanged.Invoke();
    }

    #endregion
}
