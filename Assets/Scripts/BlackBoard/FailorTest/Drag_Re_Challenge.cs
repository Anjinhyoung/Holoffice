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
                // ����� ����
                block = true;
                // print("����" + mouse_Start + "��" + mouse_End);
            }
            else
            {
                // ����� ������ ����
                block = false;
            }

            print("����" + mouse_Start + "��" + mouse_End);
        }
    }
}
