using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveSpawner : MonoBehaviour
{
    public Transform[] points;
    public Wave[] wave;
    private int _currWaveIndex;
    [SerializeField] private TextMeshProUGUI _waveCounterText;

    [SerializeField] private float spawnRate;
    private float _spawnCounter;
    private int _enemyCounter;
    [SerializeField] private TextMeshProUGUI _enemyCounterText;
    private int _GUIenemyCounter;
    [SerializeField] private float _breakTimebtwWaves;
    private float _breakCounter;
    [SerializeField] private TextMeshProUGUI _breakCounterText;
    [SerializeField] private GameObject _breakCounterPan;
    private bool _isBreak;

    private int _allEnemyCount;
    private int _allEnemyCounter;
    private void Start()
    {
        GlobalEvent.OnEnemyDestroy += DecreaseEnemy;
        _enemyCounterText.text = "0";
        _waveCounterText.text = (_currWaveIndex + 1).ToString();
        foreach (var w in wave)
        {
            _allEnemyCount += w.EnemyCount;
        }

    }
    private void Update()
    {
        if (_GUIenemyCounter == 0 && _allEnemyCounter == _allEnemyCount)
        {
            GlobalEvent.InvokeOnWin();
            return;
        }
        if (_currWaveIndex < wave.Length && _enemyCounter == wave[_currWaveIndex].EnemyCount)
        {
            StartCoroutine(BreakBetweenWaves());
        }
        if (_isBreak)
        {
            _breakCounter -= Time.deltaTime;
            _breakCounterText.text = ((int)_breakCounter).ToString();
            return;
        }
        GenerateEnemy();

    }

    void GenerateEnemy()
    {
        if (_spawnCounter > spawnRate && _enemyCounter < wave[_currWaveIndex].EnemyCount)
        {
            Enemy e = Instantiate(wave[_currWaveIndex].enemyPrefab, points[0].transform.position, Quaternion.LookRotation(points[1].transform.position - points[0].transform.position));
            e.points = points;

            _spawnCounter = 0;
            _enemyCounter++;
            _GUIenemyCounter++;
            _allEnemyCounter++;
            _enemyCounterText.text = _GUIenemyCounter.ToString();
        }

        if (_spawnCounter <= spawnRate)
            _spawnCounter += Time.deltaTime;
    }
    void DecreaseEnemy()
    {
        _GUIenemyCounter--;
        _enemyCounterText.text = _GUIenemyCounter.ToString();
    }

    IEnumerator BreakBetweenWaves()
    {
        _breakCounter = _breakTimebtwWaves;
        _isBreak = true;
        _enemyCounter = 0;
        _breakCounterPan.SetActive(true);
        yield return new WaitForSeconds(_breakTimebtwWaves);
        if (_currWaveIndex < wave.Length)
        {
            _currWaveIndex++;
            _waveCounterText.text = (_currWaveIndex + 1).ToString();
            _isBreak = false;

        }
        _breakCounterPan.SetActive(false);
    }
    private void OnDestroy()
    {
        GlobalEvent.OnEnemyDestroy -= DecreaseEnemy;
    }
}
