using UnityEngine;
using UnityEngine.UI;

public class Erase_Refactoring : MonoBehaviour
{
    [SerializeField]
    Image blackboard; // blackboard(지우개 색깔 구할려고)

    [SerializeField]
    RawImage paint; // 그림판
    RectTransform paint_RT; // 그림판 크기

    int pixel_Width, pixel_Height;

    Color erase_Color; // 지우개 색깔(blackboard의 영향을 받는다.)

    void Awake()
    {
        paint_RT = paint.GetComponent<RectTransform>();
        pixel_Width = (int)paint_RT.rect.width;
        pixel_Height = (int)paint_RT.rect.height;
        // 갑자기 static과 그냥 전역 변수 설정한 거 갑자기 궁금해짐 이것도 선생님 한테 질문해보기

        erase_Color = blackboard.color;

        erase_Button = GetComponent<Button>();
    }

    public static bool erase_Active = false;
    public static Button erase_Button;
    public void Button_On()
    {
        erase_Active = true;

        // 지우개 버튼이 활성화 되었을 경우 지우개 버튼 색깔 녹색으로 바꾸기
        ColorBlock change_Button_Color = erase_Button.colors;
        change_Button_Color.normalColor = Color.green;
        erase_Button.colors = change_Button_Color;

        // pen 버튼 하얀색으로 바꾸고 비활성화하기
        Drawing_Refactoring.pen_Active = false;
        Drawing_Refactoring.Button_Off();

        // 휴지통 버튼 하얀색으로 바꾸고 비활성화하기
        trash.trash_Active = false;
        trash.Button_Off();
    }

    public static void Button_Off()
    {
        // pen 버튼이 활성화 되었을 경우 지우개 버튼 색깔 하얀색으로 바꾸기
        ColorBlock change_Button_Color = erase_Button.colors;
        change_Button_Color.normalColor = Color.white;
        erase_Button.colors = change_Button_Color;
    }

    Vector2 lastPosition, currentPosition = Vector2.zero; // 마우스 이전 위치, 현재 위치 (초기값은 제로)
    Vector2 mousePosition; // ScreenPointToLocalPointInRectangle() 이 함수로 바뀐 마우스 좌표


    void Update()
    {
        // 지우개 버튼 눌렀을 때만 활성화
        if (erase_Active)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(paint_RT, Input.mousePosition, null, out currentPosition);

            Vector2 mousePosition = new Vector2
            (
                (currentPosition.x / pixel_Width + 0.5f) * pixel_Width,
                (currentPosition.y / pixel_Height + 0.5f) * pixel_Height
            );

            if (Input.GetMouseButtonDown(0))
            {
                // 딱 버튼 한 번만 눌렀을 경우(점 하나만 지울 경우)
                Erase_Jum(mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                // 버튼을 계속 눌렀을 경우(라인으로 지웠을 경우)
                Erase_Line(lastPosition, mousePosition);
            }
            Drawing_Refactoring.pixel_Texture.Apply();
        }
    }

    void Erase_Jum(Vector2 mousePosition_)
    {
        int brush_Width = (int)mousePosition_.x;
        int brush_Height = (int)mousePosition_.y;

        for (int height_Plus = -10; height_Plus <= 10; height_Plus++)
        {
            for (int width_Plus = -10; width_Plus <= 10; width_Plus++)
            {
                if (brush_Height + height_Plus >= 0 && brush_Height + height_Plus < pixel_Height
                    && brush_Width + width_Plus >= 0 && brush_Width + width_Plus < pixel_Width)
                {
                    Drawing_Refactoring.pixel_Texture.SetPixel(brush_Width + width_Plus, brush_Height + height_Plus, erase_Color);
                }
            }
        }
        lastPosition = mousePosition_;
    }

    void Erase_Line(Vector2 lastPosition_, Vector2 currentPosition_)
    {
        float distance = Vector2.Distance(currentPosition_, lastPosition_);

        // 간격
        float interval = 1.0f / distance; // 이거를 근데 실수형으로 해도 괜찮나...?

        // 1로 나누는 이유는 두 점 사이를 정확하게 나누고 싶을 때 1을 사용하는 거지

        for (float count = 0.0f; count <= distance; count++)
        {
            Vector2 Line = Vector2.Lerp(lastPosition_, currentPosition_, interval * count);
            Erase_Jum(Line);
        }
    }
}
