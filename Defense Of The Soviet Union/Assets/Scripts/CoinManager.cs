using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void AddCoins(int value);


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
    private void Start()
    {
        transform.parent = null;
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

    public void ShowAdvBtn()
    {
        AddCoins(300);
    }
    public void AddCoinsInGame(int value)
    {
        _coins += value;
        _coinsText.text = _coins.ToString();
        Progress.Instance.PlayerInfo.Coin = _coins;
        Progress.Instance.Save();
    }
}
