using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Waves[] _waves;
    [SerializeField] private TMP_Text _textWaves;
    [SerializeField] private Button _button;
    [SerializeField] private Canvas _canvas;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;

    private void Start()
    { 
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        LaunchWave();

    }

    private IEnumerator SpawnEnemyInWaves()
    {
        if (_enemiesLeftToSpawn > 0)
        {
            yield return new WaitForSeconds(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].SpawnDelay);

            Instantiate(_waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].Enemy, 
                _waves[_currentWaveIndex].WaveSettings[_currentEnemyIndex].NeededSpawner.transform.position, Quaternion.identity);
             
            _enemiesLeftToSpawn--;
            _currentEnemyIndex++;

            StartCoroutine(SpawnEnemyInWaves());
            _textWaves.text = "Волна" + _currentWaveIndex.ToString();
        }
        else
        {

            if (_currentWaveIndex < _waves.Length - 1)
            {
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
            else
            {
                Invoke("ActiveBtn", 20f);
            }
        }
    } 
    public void ActiveBtn()
    {

        _button.enabled = true;
        _canvas.enabled = true;
    }

    public void LaunchWave()
    {
        StartCoroutine(SpawnEnemyInWaves());
        Progress.Instance.Save();
    }
}

[System.Serializable]

public class Waves
{
    [SerializeField] private WaveSettings[] _waveSettings;
    public WaveSettings[] WaveSettings { get => _waveSettings; }
}

[System.Serializable]
public class WaveSettings
{
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _neededSpawner;
    [SerializeField] private float _spawnDelay;

    public GameObject Enemy { get => _enemy; }
    public GameObject NeededSpawner { get => _neededSpawner; }
    public float SpawnDelay { get => _spawnDelay; }

} 