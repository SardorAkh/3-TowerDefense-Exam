using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private Button _upgradeBtn;
    [SerializeField] private TextMeshProUGUI _upgradePriceText;
    [SerializeField] private TextMeshProUGUI _sellPriceText;
    [SerializeField] private Button _sellBtn;

    private Money _money;

    private bool isMenuOpened;
    private Animator _anim;
    private Cell _cell;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        GlobalEvent.OnTowerSelect += MenuOpen;
        GlobalEvent.OnChangeMoney += CheckAbleToUpgrade;
        _upgradeBtn.onClick.AddListener(UpgradeTower);
        _sellBtn.onClick.AddListener(SellTower);
        _money = FindObjectOfType<Money>();
        GlobalEvent.CloseUIMenus += MenuClose;

    }
    void UpgradeTower()
    {
        _cell.UpgradeTower();
        MenuClose();
    }
    void SellTower()
    {
        _cell.SellTower();
        MenuClose();
    }
    void MenuOpen(Tower t)
    {
        GlobalEvent.InvokeCloseUIMenus();
        _cell = t._cell;
        _upgradePriceText.text = _cell.towerInfo.towerUpgradesPrefab[_cell.upgradeStage + 1].GetComponentInChildren<Tower>().price.ToString() + "$";

        float refund = 0;
        for (int i = 0; i < _cell.upgradeStage; i++)
        {
            refund += _cell.towerInfo.towerUpgradesPrefab[i].GetComponent<Tower>().price;
        }
        if (_cell.upgradeStage == 0)
            refund = t.price;
        refund *= 0.7f;
        _sellPriceText.text = ((int)refund).ToString() + "$";
        CheckAbleToUpgrade();

        _anim.SetBool("ActiveState", true);
        isMenuOpened = true;
    }
    public void MenuClose()
    {
        if (isMenuOpened)
        {
            _cell = null;
            _anim.SetBool("ActiveState", false);
            isMenuOpened = false;
        }
    }
    void CheckAbleToUpgrade()
    {
        if (_cell)
        {
            if (_money.MoneyCount < _cell.towerInfo.towerUpgradesPrefab[_cell.upgradeStage + 1].GetComponentInChildren<Tower>().price
                && _cell.upgradeStage < _cell.maxUpgradeStages)
            {
                _upgradeBtn.interactable = false;
            }
            else
            {
                _upgradeBtn.interactable = true;
            }
        }
    }

    private void OnDestroy()
    {
        GlobalEvent.OnTowerSelect -= MenuOpen;
        GlobalEvent.OnChangeMoney -= CheckAbleToUpgrade;
        GlobalEvent.CloseUIMenus -= MenuClose;
    }
}
