using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _finishWindow;
    

    public void ShowFinishWindow()
    {
        _finishWindow.SetActive(true);
    }

    public void NewLevel() //Будет новую игру загружать и включать след. лвл
    {
        int next = SceneManager.GetActiveScene().buildIndex + 1; 
        if(next < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(next);
        }
    }
}
