using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Iternal")]
    private static extern void RateGame();

    public void RateGameButton()
    {
        RateGame();
    }

}