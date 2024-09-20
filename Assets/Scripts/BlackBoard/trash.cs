using UnityEngine;
using UnityEngine.UI;

public class trash : MonoBehaviour
{

    public static Button trash_Button;
    public static bool trash_Active;

    [SerializeField]
    RectTransform paint_RT; // 그림판 크기
    [SerializeField]
    Image blackBoard; // 색깔 구하기 위해서

    private void Awake()
    {
        trash_Button = GetComponent<Button>();
    }

    public void Button_On()
    {
        trash_Active = true;

        // 펜, 지우개 다 비활성화 하고 색깔도 하얀색으로 바꾸기
        Drawing_Refactoring.pen_Active = false;
        Erase_Refactoring.erase_Active = false;

        Drawing_Refactoring.Button_Off();
        Erase_Refactoring.Button_Off();

        // 휴지통 색깔 초록색으로 바꾸기
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
                // 여기서 알게 된 점 SetPixel은 특정 좌표에 있는 단일 픽셀의 색상을 설정한다.
                Drawing_Refactoring.pixel_Texture.SetPixel(x, y, blackBoard.color);
            }
        }
        Drawing_Refactoring.pixel_Texture.Apply();
    }
}
