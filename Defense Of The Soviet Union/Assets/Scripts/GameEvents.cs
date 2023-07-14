using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    private static GameEvents _instance;
    public static GameEvents Instance { get => _instance; private set {;} }
    public event Action onCoinChange;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void OnCoinsChange()
    {
        if (onCoinChange != null)
        {
            onCoinChange();
        }
    }
}
