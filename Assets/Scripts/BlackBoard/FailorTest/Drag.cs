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
                // 블록을 만듦
                block = true;
                // print("시작" + mouse_Start + "끝" + mouse_End);
            }
            else
            {
                // 블록을 만들지 않음
                block = false;
            }
        }
        // print("시작" + mouse_Start + "끝" + mouse_End);
        
    }
}
