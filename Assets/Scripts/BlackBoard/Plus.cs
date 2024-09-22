using UnityEngine;
using TMPro;

public class Plus : MonoBehaviour
{

    [SerializeField]
    TMP_Text note;

    public void Button_On()
    {
        note.fontSize += 5;
    }
}
