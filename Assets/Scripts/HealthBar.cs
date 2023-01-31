using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] private Image foreground;
    void Awake()
    {
        _cam = Camera.main;
    }
    
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _cam.transform.position);
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth) {
        foreground.fillAmount = currentHealth / maxHealth;
    }
}
