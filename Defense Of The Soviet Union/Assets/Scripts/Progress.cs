using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using YG;

[System.Serializable]
public class PlayerInfo
{
    public int Coin;
    public int Level;
    public Buildings[,] _grid;
}


public class Progress : MonoBehaviour
{
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
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
