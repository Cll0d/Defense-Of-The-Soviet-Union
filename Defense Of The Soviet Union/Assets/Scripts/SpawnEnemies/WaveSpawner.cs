using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] public Waves[] _waves;
    [SerializeField] private TMP_Text _textWaves;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _canvas;
    [SerializeField] GameObject Replay;
    private int _currentEnemyIndex;
    private int _currentWaveIndex;
    private int _enemiesLeftToSpawn;
    private int _activeScene;

    private void Start()
    { 
        _enemiesLeftToSpawn = _waves[0].WaveSettings.Length;
        LaunchWave();
        _activeScene = SceneManager.GetActiveScene().buildIndex;
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
            _textWaves.text = "Волна " + _currentWaveIndex.ToString();
        }
        else
        {
            if (_currentWaveIndex < _waves.Length - 1)
            {
                Debug.Log("next volna");
                _currentWaveIndex++;
                _enemiesLeftToSpawn = _waves[_currentWaveIndex].WaveSettings.Length;
                _currentEnemyIndex = 0;
            }
            else
            {
                Debug.Log("net volna");
                if (_activeScene == 1)
                    Invoke("ActiveBtn", 15f);
                else
                    Invoke("ReplayCanvas", 10f);
            }
        }
    }
    public void ActiveBtn()
    {
        _button.enabled = true;
        _canvas.SetActive(true);
    }

    public void LaunchWave()
    {
        if(gameObject != null)
        {
            StartCoroutine(SpawnEnemyInWaves());
        }
    }
    public void ReplayCanvas()
    {
        Replay.SetActive(true);
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