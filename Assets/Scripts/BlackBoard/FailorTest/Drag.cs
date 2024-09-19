using UnityEngine;
using TMPro;  

public class Drag : MonoBehaviour
{
    TMP_InputField inputField; 
    public int mouse_Start, mouse_End;

    public bool drag_on = false;
    public bool block = false;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();  
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && inputField.isFocused)
        {
            mouse_Start = inputField.selectionAnchorPosition;
            
        }

        if (Input.GetMouseButton(0) && inputField.isFocused)
        {
            mouse_End = inputField.selectionFocusPosition;
        }

        if (Input.GetMouseButtonUp(0) && inputField.isFocused)
        {
            if (mouse_Start > mouse_End)
            {
                int temp = mouse_Start;
                mouse_Start = mouse_End;
                mouse_End = temp;
            }

            if (mouse_Start != mouse_End)
            {
                // ����� ����
                block = true;
                // print("����" + mouse_Start + "��" + mouse_End);
            }
            else
            {
                // ����� ������ ����
                block = false;
            }
        }
        // print("����" + mouse_Start + "��" + mouse_End);
        
    }
}
