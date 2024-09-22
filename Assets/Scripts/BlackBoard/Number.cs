using UnityEngine;
using TMPro;

public class Number : MonoBehaviour
{
    TMP_Text size;

    [SerializeField]
    TMP_Text note_FontSize;

    private void Awake()
    {
        size = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        size.text = note_FontSize.fontSize.ToString();
    }
}
