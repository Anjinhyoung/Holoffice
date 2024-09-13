using UnityEngine;
using TMPro;

public class MaintainFocus : MonoBehaviour
{
    public TMP_InputField inputField;

    // ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnButtonClick()
    {
        
        // InputField�� ��Ŀ�� ����
        inputField.Select();
        inputField.ActivateInputField();

        // Ŀ���� ������ �Է� ��ġ�� ����
        inputField.caretPosition = inputField.text.Length;
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;
        

    }
}