using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Detector : MonoBehaviour
{
    public GraphicRaycaster raycaster;
    public static bool ok = false;

    void Update()
    {
        CheckInputFieldInteraction();
    }

    private void CheckInputFieldInteraction()
    {
        if (raycaster != null)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            ok = false;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.CompareTag("Note"))
                {
                    ok = true;
                    break;
                }
            }
        }
    }
}