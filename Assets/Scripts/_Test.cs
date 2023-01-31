using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Test : MonoBehaviour
{
    
    [SerializeField] private BuildMenu _b;

    void OnMouseDown() {
        _b.MenuClose();
    }
}
