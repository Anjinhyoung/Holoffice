using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Italic : MonoBehaviour
{
    [SerializeField]
    TMP_Text italic_note;

    public bool italic_Active = false;

    Button italic_Button;
    Button bold_Button;
    Bold_Re_Challenge bold;

    private void Awake()
    {
        italic_Button = GetComponent<Button>();
        bold_Button = GameObject.Find("Language_button").GetComponent<Button>();
        bold = bold_Button.GetComponent<Bold_Re_Challenge>();
    }

    public void Button_On()
    {
        // 만약 활성화 안 되어 있다면
        if(italic_Active == false)
        {

            // bold체가 활성화 안 되어 있다면 이태릭만 하기
            if(bold.bold_Active == false)
            {
                italic_Active = true;

                ColorBlock change_Button_Color = italic_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                italic_Button.colors = change_Button_Color;

                italic_note.fontStyle = FontStyles.Italic;
            }

            // bold체가 활성화 되어 있다면 이태릭도 추가 하기
            else
            {
                italic_Active = true;

                ColorBlock change_Button_Color = italic_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                italic_Button.colors = change_Button_Color;

                italic_note.fontStyle = FontStyles.Bold | FontStyles.Italic;
            }
        }

        // 만약 활성화 되어 있다면
        else
        {
            // bold체는 활성화 되어 있고 이태릭체만 없애고 싶은 경우
            if (bold.bold_Active)
            {
                italic_Active = false;

                ColorBlock change_Button_Color = italic_Button.colors;
                change_Button_Color.normalColor = Color.white;
                change_Button_Color.selectedColor = Color.white;
                italic_Button.colors = change_Button_Color;

                italic_note.fontStyle = FontStyles.Bold;
            }

            else
            {
                italic_Active = false;

                ColorBlock change_Button_Color = italic_Button.colors;
                change_Button_Color.normalColor = Color.white;
                change_Button_Color.selectedColor = Color.white;
                italic_Button.colors = change_Button_Color;

                italic_note.fontStyle = FontStyles.Normal;
            }
        }
    }
}
