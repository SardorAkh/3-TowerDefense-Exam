using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level level;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private TextMeshProUGUI _WaveCounterText;
    void Start()
    {
        waveSpawner.points = level.points;
        waveSpawner.wave = level.Waves;
    }
    void Update()
    {

    }
}
