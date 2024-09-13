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

        mouse_Start = drag.mouse_Start; // 커서 시작점
        mouse_End = drag.mouse_End; // 커서 끝점
        block = drag.block; // block을 만들었는 지 안 만들었는 지 유부

        // 드래그 한 부분이 <b>가 있는 지 없는 지 확인하기 
        if (block)
        {   

            // 확인하는 방법은 mouse_End - mouse_start + 3 이 그 drag한 글자 길이랑 같으면 bold체로 판별
            if (mouse_End - mouse_Start + 3 == drag_InputField.text.Length)
            {
                
            }
            // 만약 mouse_End - mouse_start + 7이 drag한 글자 길이(drag_InputField.text.length)랑 같으면 bold체로 판별
            else if(mouse_End - mouse_Start + 7 == drag_InputField.text.Length)
            {

            }
            // 또한 mouse_End -mouse_Start == drag_InputField.text.length 길이가 같으면 bold체가 아닌 걸로 판별
            else if(mouse_End - mouse_Start == drag_InputField.text.Length)
            {
                string before_Text = drag_InputField.text.Substring(0, mouse_Start);
                string bold_Text = drag_InputField.text.Substring(mouse_Start, mouse_End - mouse_Start);
                string next_Text = drag_InputField.text.Substring(mouse_End);
                drag_InputField.text = before_Text + "<b>" + bold_Text + "</b>" + next_Text;
            }
        }

        

        // 드래그 없이 이후 입력되는 텍스트를 bold 처리, 해제할 경우
        else if (block == false)
        {
            // 앵커 뒤에? 앞에?  "<b>"가 있는지 확인 => 근데 그거를 도대체 어떻게 확인해야 하지......?
            if (false)
            {
                // 앵커 뒤에? 앞에? "<b>"가 있으면 </b>를 삽입하거나 replace를 이용해서 <b>를 삭제하기
            }
            else
            {
                // 앵커 뒤에? 앞에? "<b>"가 없으면 앵커 뒤에? 앞에? <b>를 삽입?
            }
        }
    }
}