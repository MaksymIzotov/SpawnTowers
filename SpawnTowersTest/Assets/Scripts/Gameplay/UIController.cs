using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    private MoneyController moneyController;
    private TowerPriceController towerPriceController;

    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private TMP_Text towerPriceText;

    private void Start()
    {
        moneyController = GetComponent<MoneyController>();
        towerPriceController = GetComponent<TowerPriceController>();
    }

    public void SetMoneyText()
    {
        moneyText.text = moneyController.GetAmount().ToString();
    }

    public void SetPriceText()
    {
        towerPriceText.text = towerPriceController.GetAmount().ToString();
    }
}
