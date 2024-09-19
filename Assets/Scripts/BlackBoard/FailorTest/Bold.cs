using UnityEngine;
using TMPro;
public class Bold : MonoBehaviour
{
    public TMP_InputField drag_InputField;
    Drag drag;
    int mouse_Start, mouse_End;
    bool block;

    private void Awake()
    {
        drag = drag_InputField.GetComponent<Drag>();
    }
    public void Bold_Check()
    {

        mouse_Start = drag.mouse_Start; // Ŀ�� ������
        mouse_End = drag.mouse_End; // Ŀ�� ����
        block = drag.block; // block�� ������� �� �� ������� �� ����

        // �巡�� �� �κ��� <b>�� �ִ� �� ���� �� Ȯ���ϱ� 
        if (block)
        {   

            // Ȯ���ϴ� ����� mouse_End - mouse_start + 3 �� �� drag�� ���� ���̶� ������ boldü�� �Ǻ�
            if (mouse_End - mouse_Start + 3 == drag_InputField.text.Length)
            {
                
            }
            // ���� mouse_End - mouse_start + 7�� drag�� ���� ����(drag_InputField.text.length)�� ������ boldü�� �Ǻ�
            else if(mouse_End - mouse_Start + 7 == drag_InputField.text.Length)
            {

            }
            // ���� mouse_End -mouse_Start == drag_InputField.text.length ���̰� ������ boldü�� �ƴ� �ɷ� �Ǻ�
            else if(mouse_End - mouse_Start == drag_InputField.text.Length)
            {
                string before_Text = drag_InputField.text.Substring(0, mouse_Start);
                string bold_Text = drag_InputField.text.Substring(mouse_Start, mouse_End - mouse_Start);
                string next_Text = drag_InputField.text.Substring(mouse_End);
                drag_InputField.text = before_Text + "<b>" + bold_Text + "</b>" + next_Text;
            }
        }

        

        // �巡�� ���� ���� �ԷµǴ� �ؽ�Ʈ�� bold ó��, ������ ���
        else if (block == false)
        {
            // ��Ŀ �ڿ�? �տ�?  "<b>"�� �ִ��� Ȯ�� => �ٵ� �װŸ� ����ü ��� Ȯ���ؾ� ����......?
            if (false)
            {
                // ��Ŀ �ڿ�? �տ�? "<b>"�� ������ </b>�� �����ϰų� replace�� �̿��ؼ� <b>�� �����ϱ�
            }
            else
            {
                // ��Ŀ �ڿ�? �տ�? "<b>"�� ������ ��Ŀ �ڿ�? �տ�? <b>�� ����?
            }
        }
    }
}