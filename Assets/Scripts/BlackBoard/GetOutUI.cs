using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutUI : MonoBehaviour
{
    public Canvas canvas;
    public void GetOut()
    {
        canvas.gameObject.SetActive(false);
    }
}
