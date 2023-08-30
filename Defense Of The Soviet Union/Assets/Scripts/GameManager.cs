using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ShowAdv();
    [DllImport("__Internal")]
    private static extern void RateGame();
    [SerializeField] GameObject _finishWindow;

    

    public void ShowFinishWindow()
    {
        _finishWindow.SetActive(true);
    }

    public void NewLevel() //Будет новую игру загружать и включать след. лвл
    {
        SceneManager.LoadScene(Progress.Instance.PlayerInfo.Level + 1);

        int next = SceneManager.GetActiveScene().buildIndex + 1; 
        if(next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
    }

    public void NextLevel()
    {
        ShowAdv();
        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if(next < SceneManager.sceneCountInBuildSettings)
        {
            Progress.Instance.PlayerInfo.Level = SceneManager.GetActiveScene().buildIndex;
        }
        Progress.Instance.Save();
        SceneManager.LoadScene(next);
    }
    public void RateGameBtn()
    {
        RateGame();
    }

    public void ShowAdvBtn()
    {
        
    }
}
