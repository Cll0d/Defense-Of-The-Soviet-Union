using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWaves : MonoBehaviour
{
    private WaveSpawner _waveSpawner;

    private void Awake()
    {
        _waveSpawner = GameObject.Find("WaveController").GetComponent<WaveSpawner>();
    }

    private void OnDestroy()
    {
        int enemiseLeft = 0;
        enemiseLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiseLeft == 0)
        {
            _waveSpawner.LaunchWave();
        }
    }
}
