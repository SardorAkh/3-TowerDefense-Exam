using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()

    {
        RaycastOnMouseDown();
    }

    void RaycastOnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits = Physics.RaycastAll(_camera.ScreenPointToRay(Input.mousePosition));
            if (hits.Length == 0)
                return;


            if (EventSystem.current.IsPointerOverGameObject()) // is the touch on the GUI
            {
                return;
            }

            Collider h;
            for (int i = 0; i < hits.Length; i++)
            {
                h = hits[i].collider;

                if (h.CompareTag("Cell"))
                {

                    Cell c = h.gameObject.GetComponent<Cell>();
                    if (c.IsEmpty)
                    {
                        GlobalEvent.InvokeOnCellSelect(c);
                    }
                }
                else if (h.CompareTag("Tower"))
                {
                    GlobalEvent.InvokeOnTowerSelect(h.gameObject.GetComponentInParent<Tower>());
                }
            }
        }
    }

}
