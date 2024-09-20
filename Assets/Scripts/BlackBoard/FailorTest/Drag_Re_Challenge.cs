using UnityEngine;
using TMPro;
using Photon.Pun.Demo.Procedural;

public class Drag_Re_Challenge : MonoBehaviour
{
    TMP_InputField note;
    public int mouse_Start, mouse_End;
    public bool block;

    private void Awake()
    {
        note = GetComponent<TMP_InputField>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouse_Start = note.selectionAnchorPosition;
        }

        if (Input.GetMouseButton(0))
        {
            mouse_End = note.selectionFocusPosition;
        }

        if (Input.GetMouseButtonUp(0))
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

            print("시작" + mouse_Start + "끝" + mouse_End);
        }
    }
}
