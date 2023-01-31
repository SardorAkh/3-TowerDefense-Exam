using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Health : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _health; 
    void Start()
    {
        _text.text = _health.ToString();
        GlobalEvent.OnDecreaseHealth += DecreaseHealth;
    }

    void DecreaseHealth(int amount) {
        _health -= amount;
        _text.text = _health.ToString();
        CheckLose();
    }
    void CheckLose() {
        if(_health <= 0) {
            GlobalEvent.InvokeOnLose();
        }
    }

    void OnDestroy() {
        GlobalEvent.OnDecreaseHealth -= DecreaseHealth;
    }
}
