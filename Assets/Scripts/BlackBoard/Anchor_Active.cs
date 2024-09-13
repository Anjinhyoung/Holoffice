using UnityEngine;
using TMPro;

public class MaintainFocus : MonoBehaviour
{
    public TMP_InputField inputField;

    // 버튼 클릭 시 호출되는 메서드
    public void OnButtonClick()
    {
        
        // InputField에 포커스 설정
        inputField.Select();
        inputField.ActivateInputField();

        // 커서를 마지막 입력 위치로 설정
        inputField.caretPosition = inputField.text.Length;
        inputField.selectionAnchorPosition = inputField.text.Length;
        inputField.selectionFocusPosition = inputField.text.Length;
        

    }
}