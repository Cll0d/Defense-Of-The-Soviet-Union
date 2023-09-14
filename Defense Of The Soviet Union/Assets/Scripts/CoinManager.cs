using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using YG;

public class CoinManager : MonoBehaviour
{
    [SerializeField] int Id = 0;
    [SerializeField] private int _coins;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private int _startCoins;
    public int Coins { get => _coins; }
    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }  
    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }
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
    }
    public void Rewarded(int id)
    {
        if(id == Id)
        {
            AdMoney(300);
        }
    }
    void AdMoney(int count)
    {
        _coins += count;
        _coinsText.text = "" + _coins;
    }
    public void AddCoinsInGame(int value)
    {
        _coins += value;
        _coinsText.text = _coins.ToString();
        Progress.Instance.PlayerInfo.Coin = _coins;
    }
    public void ShowAdd()
    {
        YandexGame.RewVideoShow(Id);
    }
}
