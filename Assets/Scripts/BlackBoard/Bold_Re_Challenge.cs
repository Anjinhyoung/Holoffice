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
        // ��ư�� Bold ü�� �ƴ� ���
        if(bold_Active == false)
        {
            // ���¸�ü�� Ȱ��ȭ �� �Ǿ� �־��ٸ� boldü�� �ϱ�
            if(italic.italic_Active == false)
            {
                // Bold ��ư �ʷϻ����� �ٲٱ�
                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                bold_Button.colors = change_Button_Color;

                // Ȱ��ȭ
                bold_Active = true;

                // ���� �ϱ�
                note_bold.fontStyle = FontStyles.Bold;
            }
            else
            {
                // Bold ��ư �ʷϻ����� �ٲٱ�
                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.green;
                change_Button_Color.selectedColor = Color.green;
                bold_Button.colors = change_Button_Color;

                // Ȱ��ȭ
                bold_Active = true;

                // ���� �ϰ� ���¸� �ϱ� (�÷���)�� ���ǵȴٴ� ���� �������� ��Ʈ ������ ���� ���� �� �ְ� �Ǿ
                // �� ���� �������� ��Ʈ�� ����ϰ� �ǰ�, ���� ���� ���ÿ� ������ �� �ְ� �ȴ�.
                note_bold.fontStyle = FontStyles.Bold | FontStyles.Italic;

            }
        }

        // �̹� bold ü �ϰ��
        else
        {

            // ���¸�ü�� Ȱ��ȭ �Ǿ� �ְ� boldü�� ���ְ� ���� ���
            if (italic.italic_Active)
            {
                // ��Ȱ��ȭ
                bold_Active = false;

                ColorBlock change_Button_Color = bold_Button.colors;
                change_Button_Color.normalColor = Color.white;
                change_Button_Color.selectedColor = Color.white;
                bold_Button.colors = change_Button_Color;

                note_bold.fontStyle = FontStyles.Italic;
            }

            // ���¸��� ��Ȱ��ȭ bold�� ��Ȱ��ȭ �ϰ� ���� ���
            else
            {
                // ��Ȱ��ȭ
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
