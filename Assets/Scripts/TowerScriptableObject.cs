using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "_ScriptableObjects/TowerInfo", fileName = "TowerInfo", order = 0)]
public class TowerScriptableObject : ScriptableObject
{
    public List<TowerInfo> towers;

}

[System.Serializable]
public struct TowerInfo
{
    public GameObject towerPrefab;
    public Sprite sprite;
    public List<GameObject> towerUpgradesPrefab;
}
