using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _money;
    public int MoneyCount
    {

        get => _money;

        set
        {
            _money = value;
            GlobalEvent.InvokeOnChangeMoney();
        }
    }
    void Start()
    {
        _text.text = _money.ToString();
        GlobalEvent.OnIncreaseMoney += IncreaseMoney;
        GlobalEvent.OnDecreaseMoney += DecreaseMoney;
    }
    void IncreaseMoney(int amount)
    {
        MoneyCount += amount;
        _text.text = _money.ToString();
    }
    void DecreaseMoney(int amount)
    {
        MoneyCount -= amount;
        _text.text = _money.ToString();
    }
    void OnDestroy()
    {
        GlobalEvent.OnIncreaseMoney -= IncreaseMoney;
        GlobalEvent.OnDecreaseMoney -= DecreaseMoney;
    }
}
