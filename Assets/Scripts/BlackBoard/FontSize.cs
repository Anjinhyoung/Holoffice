using UnityEngine;
using TMPro;

public class FontSize : MonoBehaviour
{
    [SerializeField]
    TMP_Text note;

    public void Button_On()
    {
        note.fontSize += 10;
    }

}
