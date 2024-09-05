using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    public Canvas canvas;

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            canvas.gameObject.SetActive(true);
        }
    }
}
