using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class PlayerInfo
{
    public int Coin;
    public int Level;
    public Buildings[,] _grid;
}


public class Progress : MonoBehaviour
{
    [DllImport ("__Internal")]
    public static extern void SaveExtern(string date);
    [DllImport("__Internal")]
    public static extern void LoadExtern();

    [SerializeField] TextMeshProUGUI _playerInfoText;

    public PlayerInfo PlayerInfo;

    public static Progress Instance;
    private void Awake()
    {
        
        if(Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
            LoadExtern();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonstring = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonstring);
    }

    public void Load(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = PlayerInfo.Level + "\n" + PlayerInfo.Coin + "\n" + PlayerInfo._grid;
    }
    public void LoadDate()
    {
        LoadExtern(); 
    }
}
