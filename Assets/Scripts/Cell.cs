using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private GameObject Quad;
    public bool IsEmpty { get; private set; }

    [SerializeField] private Transform towerRespawnPoint;
    public TowerInfo towerInfo;
    private GameObject tower;
    [SerializeField] private Material standardMat;
    [SerializeField] private Material selectedMat;
    public int upgradeStage;
    public int maxUpgradeStages;
    private void Start()
    {
        GlobalEvent.OnCellSelect += SelectCell;
        IsEmpty = true;
    }
    public void BuildTower(TowerInfo t)
    {
        if (!IsEmpty)
            return;
        Tower towerPrefab = t.towerUpgradesPrefab[0].GetComponent<Tower>();
        IsEmpty = false;
        Quad.SetActive(false);
        upgradeStage = 0;
        GameObject prefab = Instantiate(t.towerPrefab, towerRespawnPoint.position, Quaternion.identity, transform);

        GlobalEvent.InvokeOnDecreaseMoney(towerPrefab.price);
        prefab.GetComponent<Tower>()._cell = this;

        tower = prefab;
        maxUpgradeStages = t.towerUpgradesPrefab.Count;
        towerInfo = t;
    }
    public void UpgradeTower()
    {
        if (upgradeStage < maxUpgradeStages)
        {
            upgradeStage++;
            DestroyImmediate(tower);

            GameObject prefab = Instantiate(towerInfo.towerUpgradesPrefab[upgradeStage], towerRespawnPoint.position, Quaternion.identity, transform);
            prefab.GetComponentInChildren<Tower>()._cell = this;

            tower = prefab;
            GlobalEvent.InvokeOnDecreaseMoney(towerInfo.towerUpgradesPrefab[upgradeStage].GetComponentInChildren<Tower>().price);
        }
    }
    public void SellTower()
    {
        float refund = 0;
        for (int i = 0; i < upgradeStage; i++)
        {
            refund += towerInfo.towerUpgradesPrefab[i].GetComponent<Tower>().price;
        }
        if(upgradeStage == 0)
        refund = tower.GetComponent<Tower>().price;
        refund *= 0.7f;

        DestroyImmediate(tower);
        GlobalEvent.InvokeOnIncreaseMoney((int)refund);
        ResetCell();
    }
    public void SelectCell(Cell c)
    {
        if (c.Equals(this))
        {
            Quad.GetComponent<MeshRenderer>().material = selectedMat;
            return;
        }
        Quad.GetComponent<MeshRenderer>().material = standardMat;

    }
    public void ResetCell()
    {
        if (tower.Equals(null))
        {
            IsEmpty = true;
            Quad.SetActive(true);
        }
    }

    void OnDestroy()
    {
        GlobalEvent.OnCellSelect -= SelectCell;
    }
}
