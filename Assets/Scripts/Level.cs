using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject levelPrefab;
    public Transform[] points;
    public Wave[] Waves;
}
[System.Serializable]
public struct Wave
{
    public int EnemyCount;
    public Enemy enemyPrefab;
}
