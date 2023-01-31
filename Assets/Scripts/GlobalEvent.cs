using System;
using System.Collections;
using System.Collections.Generic;

public static class GlobalEvent
{
    public static Action<Cell> OnCellSelect;
    public static void InvokeOnCellSelect(Cell cell)
    {

        OnCellSelect?.Invoke(cell);
    }
    public static Action<Tower> OnTowerSelect;
    public static void InvokeOnTowerSelect(Tower tower)
    {
        OnTowerSelect?.Invoke(tower);
    }

    public static Action<int> OnDecreaseHealth;

    public static void InvokeOnDecreaseHealth(int amount)
    {
        OnDecreaseHealth?.Invoke(amount);
    }

    public static Action OnChangeMoney;

    public static void InvokeOnChangeMoney()
    {
        OnChangeMoney?.Invoke();
    }

    public static Action<int> OnIncreaseMoney;

    public static void InvokeOnIncreaseMoney(int amount)
    {
        OnIncreaseMoney?.Invoke(amount);
    }
    public static Action<int> OnDecreaseMoney;

    public static void InvokeOnDecreaseMoney(int amount)
    {
        OnDecreaseMoney?.Invoke(amount);
    }

    public static Action OnEnemyDestroy;
    public static void InvokeOnEnemyDestroy() {
        OnEnemyDestroy?.Invoke();
    }
    public static Action OnLose;
    public static void InvokeOnLose() {
        OnLose?.Invoke();
    }
    public static Action OnWin;
    public static void InvokeOnWin() {
        OnWin?.Invoke();
    }

    public static Action CloseUIMenus;
    public static void InvokeCloseUIMenus(){
        CloseUIMenus?.Invoke();
    }

    public static Action OnChangeLevel;
    
    public static void InvokeOnChangeLevel(){
        OnChangeLevel?.Invoke();
    }
}
