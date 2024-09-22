using UnityEngine;
using TMPro;

public class FontSize : MonoBehaviour
{
    [SerializeField]
    GameObject fontSize;

    bool active = false;
    public void Button_On()
    {
        active = !active;
        fontSize.SetActive(active);
    }
}
