using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int _coins;
    public int Coins { get => _coins; }
    [SerializeField] private int _startCoins;
    [SerializeField] private TMP_Text _coinsText;

    private static CoinManager _instance;
    public static CoinManager Instance { get { return _instance; } }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        _coins = _startCoins;
        _coinsText.text = _coins.ToString();
    }

    public void PayKill(int coin)
    {
        _coins += coin;
        _coinsText.text = _coins.ToString();
        GameEvents.Instance.OnCoinsChange();
    }
    public void SpendCoins(int coin)
    {
        _coins -= coin;
        _coinsText.text = _coins.ToString();
        GameEvents.Instance.OnCoinsChange();
        Progress.Instance.Save();
    }
}
