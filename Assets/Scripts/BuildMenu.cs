using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BuildMenu : MonoBehaviour
{
    [SerializeField] private GameObject contentPanel;
    [SerializeField] private TowerScriptableObject towerScriptableObject;
    [SerializeField] private Button btnPrefab;


    private bool isMenuOpened;
    private Cell _cell;
    private Tower _tower;
    private Animator _anim;
    private Money _money;
    private List<Button> buttons;
    void Awake()
    {
        _anim = GetComponent<Animator>();
    }
    void Start()
    {
        buttons = new List<Button>();
        _money = FindObjectOfType<Money>();
        foreach (TowerInfo t in towerScriptableObject.towers)
        {
            Button btn = Instantiate(btnPrefab, transform.position, Quaternion.identity, contentPanel.transform);
            btn.GetComponentsInChildren<Image>()[1].sprite = t.sprite;
            btn.GetComponentInChildren<TextMeshProUGUI>().text = t.towerPrefab.GetComponentInChildren<Tower>().price.ToString() + "$";
            btn.onClick.AddListener(() => BuildTower(t));
            buttons.Add(btn);
        }
        GlobalEvent.OnCellSelect += MenuOpen;
        GlobalEvent.OnChangeMoney += CheckAbleToBuy;
        GlobalEvent.CloseUIMenus += MenuClose;
    }
    void Update()
    {
        if (isMenuOpened && Input.GetKeyDown(KeyCode.Escape))
        {
            MenuClose();
        }
    }
    void BuildTower(TowerInfo t)
    {
        _cell.BuildTower(t);
        MenuClose();
    }
    void MenuOpen(Cell cell)
    {
        GlobalEvent.InvokeCloseUIMenus();
        _cell = cell;

        _anim.SetBool("ActiveState", true);
        isMenuOpened = true;
        CheckAbleToBuy();
    }
    public void MenuClose()
    {
        if (isMenuOpened)
        {
            _anim.SetBool("ActiveState", false);
            isMenuOpened = false;
        }
    }
    void CheckAbleToBuy()
    {
        if (isMenuOpened)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (_money.MoneyCount < towerScriptableObject.towers[i].towerPrefab.GetComponentInChildren<Tower>().price)
                {
                    buttons[i].interactable = false;
                }
                else
                {
                    buttons[i].interactable = true;
                }
            }
        }
    }

    private void OnDestroy()
    {
        GlobalEvent.OnCellSelect -= MenuOpen;
        GlobalEvent.OnChangeMoney -= CheckAbleToBuy;
        GlobalEvent.CloseUIMenus -= MenuClose;

    }
}
