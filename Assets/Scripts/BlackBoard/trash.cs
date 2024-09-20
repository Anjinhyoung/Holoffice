using UnityEngine;
using UnityEngine.UI;

public class trash : MonoBehaviour
{

    public static Button trash_Button;
    public static bool trash_Active;

    [SerializeField]
    RectTransform paint_RT; // �׸��� ũ��
    [SerializeField]
    Image blackBoard; // ���� ���ϱ� ���ؼ�

    private void Awake()
    {
        trash_Button = GetComponent<Button>();
    }

    public void Button_On()
    {
        trash_Active = true;

        // ��, ���찳 �� ��Ȱ��ȭ �ϰ� ���� �Ͼ������ �ٲٱ�
        Drawing_Refactoring.pen_Active = false;
        Erase_Refactoring.erase_Active = false;

        Drawing_Refactoring.Button_Off();
        Erase_Refactoring.Button_Off();

        // ������ ���� �ʷϻ����� �ٲٱ�
        ColorBlock change_Button_Color = trash_Button.colors;
        change_Button_Color.normalColor = Color.green;
        trash_Button.colors = change_Button_Color;

        Reset();
    }

    public static void Button_Off()
    {
        ColorBlock change_Button_Color = trash_Button.colors;
        change_Button_Color.normalColor = Color.white;
        trash_Button.colors = change_Button_Color;
    }

    private void Reset()
    {
        for (int x = 0; x < Drawing_Refactoring.pixel_Texture.width; x++)
        {
            for (int y = 0; y < Drawing_Refactoring.pixel_Texture.height; y++)
            {
                // ���⼭ �˰� �� �� SetPixel�� Ư�� ��ǥ�� �ִ� ���� �ȼ��� ������ �����Ѵ�.
                Drawing_Refactoring.pixel_Texture.SetPixel(x, y, blackBoard.color);
            }
        }
        Drawing_Refactoring.pixel_Texture.Apply();
    }
}
