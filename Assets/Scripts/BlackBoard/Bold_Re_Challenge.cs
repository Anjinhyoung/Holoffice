using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Bold_Re_Challenge : MonoBehaviour
{
    [SerializeField]
    TMP_Text note_bold;

    public bool bold_Active = false;

    Button bold_Button;
    Button italic_Button;
    Italic italic;

    private void Awake()
    {
        bold_Button = GetComponent<Button>();
        italic_Button = GameObject.Find("Language_button2").GetComponent<Button>();
        italic = italic_Button.GetComponent<Italic>();
    }

    public void Button_On()
    {
        // 버튼이 Bold 체가 아닐 경우
        if(bold_Active == false)
        {
            // 이태릭체가 활성화 안 되어 있었다면 bold체만 하기
            if(italic.italic_Active == false)
            {
                // Bold 버튼 초록색으로 바꾸기
                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                bold_Button.colors = change_Button_Color;

                // 활성화
                bold_Active = true;

                // 굵게 하기
                note_bold.fontStyle = FontStyles.Bold;
            }
            else
            {
                // Bold 버튼 초록색으로 바꾸기
                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                bold_Button.colors = change_Button_Color;

                // 활성화
                bold_Active = true;

                // 굵게 하고 이태릭 하기 (플래그)로 정의된다는 것은 열거형이 비트 단위로 값을 가질 수 있게 되어서
                // 각 값이 독립적인 비트를 사용하게 되고, 여러 값을 동시에 조합할 수 있게 된다.
                note_bold.fontStyle = FontStyles.Bold | FontStyles.Italic;

            }
        }

        // 이미 bold 체 일경우
        else
        {

            // 이태릭체는 활성화 되어 있고 bold체만 없애고 싶은 경우
            if (italic.italic_Active)
            {
                // 비활성화
                bold_Active = false;

                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.white;
                change_Button_Color.selectedColor = Color.white;
                bold_Button.colors = change_Button_Color;

                note_bold.fontStyle = FontStyles.Italic;
            }

            // 이태릭도 비활성화 bold도 비활성화 하고 싶은 경우
            else
            {
                // 비활성화
                bold_Active = false;

                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.white;
                change_Button_Color.selectedColor = Color.white;
                bold_Button.colors = change_Button_Color;

                note_bold.fontStyle = FontStyles.Normal;
            }
        }
    }
}
