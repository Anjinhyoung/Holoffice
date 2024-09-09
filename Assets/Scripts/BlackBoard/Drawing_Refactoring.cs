using UnityEngine;
using UnityEngine.UI;

public class Drawing_Refactoring : MonoBehaviour
{
    [SerializeField]
    Image blackboard; // blackboard (가져오는 이유는 blackboard의 배경색을 paint에 덧칠하기 위해서)

    [SerializeField]
    RawImage paint; // 그림판
    RectTransform paint_RT; // 그림판 크기
    Color paint_Color; // blackboard의 배경색을 paint에 덧칠하기 위해서

    public static Color draw_Color = Color.white; // 펜 색깔 (public static 으로 선언한 이유는 '화이트 보드 전환' 때문에 공유하기 위해서)

    public static Texture2D pixel_Texture; // public static 으로 선언한 이유는 '공유'를 하기 위해서(Erase Script랑 공유)
    int pixel_Width, pixel_Height;

    private void Awake() // 참조 및 초기화 여기서 한 번 물어봐야 한다. (순서 때문에)
    {

        paint_RT = paint.GetComponent<RectTransform>();
        paint_Color = blackboard.color; // 색깔쪽 한 번만 선생님한테 질문하기!

        pixel_Width = (int)paint_RT.rect.width;
        pixel_Height = (int)paint_RT.rect.height;

        pixel_Texture = new Texture2D(pixel_Width, pixel_Height);

        for (int height = 0; height < pixel_Height; height++)
        {
            for (int width = 0; width < pixel_Width; width++)
            {
                pixel_Texture.SetPixel(width, height, paint_Color); // for문 다시 한 번 선생님한테 질문한기
            }
        }

        pixel_Texture.Apply();

        paint.texture = pixel_Texture; // Apply()까지 잘 한 Texture를 그림판 Texture에 연동

        pen_Button = GetComponent<Button>();
    }

    public static bool pen_Active = false; // public static으로 설정한 이유는 Erase 함수에서도 사용해야 하기 때문에
    public static Button pen_Button; // public으로 설정한 이유는 Erase 함수에서도 사용해야 하기 때문에

    public void Button_On()
    {
        pen_Active = true;

        // pen 버튼이 활성화 되었을 경우 pen 버튼 색깔 녹색으로 바꾸기
        ColorBlock change_Button_Color = pen_Button.colors;
        change_Button_Color.normalColor = Color.green;
        pen_Button.colors = change_Button_Color;

        // 지우개 버튼 햐얀색으로 바꾸고 활성화하기
        Erase_Refactoring.erase_Active = false;
        Erase_Refactoring.Button_Off();
    }

    public static void Button_Off()
    {
        // 지우개 버튼이 활성화 되었을 경우 pen 버튼 색깔 하얀색으로 바꾸기
        ColorBlock change_Button_Color = pen_Button.colors;
        change_Button_Color.normalColor = Color.white;
        pen_Button.colors = change_Button_Color;
    }


    Vector2 lastPosition, currentPosition = Vector2.zero; // 마우스 이전 위치, 현재 위치 (초기값은 제로)
    Vector2 mousePosition; // ScreenPointToLocalPointInRectangle() 이 함수로 바뀐 마우스 좌표

    void Update()
    {
        // 버튼이 눌러졌을 경우
        if (pen_Active)
        {
            // 매개 변수로는 로컬 좌표로 변환하고 싶은 UI 요소, 변환할 스크린 좌표, 변환을 수행할 때 사용할 카메라, 변환된 로컬 좌표 값을 받을 변수
            RectTransformUtility.ScreenPointToLocalPointInRectangle(paint_RT, Input.mousePosition, null, out currentPosition);

            // print(currentPosition) 피봇이 중심으로 나온다.

            mousePosition = new Vector2
            (
                (currentPosition.x / pixel_Width + 0.5f) * pixel_Width,
                (currentPosition.y / pixel_Height + 0.5f) * pixel_Height
            ); // 이거를 꼭 바꿔주는 이유는 정확히 자기가 마우스 찍은데에서 편하게 그리기 위해서...? 이것도 선생님 한테 질문

            if (Input.GetMouseButtonDown(0)) // 마우스 딱 한 번만 눌러졌을 때
            {
                // 점 하나만 딱 찍힐 수 있게 하기
                Draw_Jum(mousePosition);
            }

            else if (Input.GetMouseButton(0)) // 마우스를 계속 누르고 있을 때
            {
                // 곡선으로 그려질 수 있게
                Draw_Line(lastPosition, mousePosition);
            }

            pixel_Texture.Apply();
        }
    }


    void Draw_Jum(Vector2 mousePosition_)
    {
        // 이것보다 더 좋은 게 있을까? 선생님한테 질문하기

        int brush_Width = (int)mousePosition_.x;
        int brush_Height = (int)mousePosition_.y;

        for (int height_Plus = -2; height_Plus <= 2; height_Plus++)
        {
            for (int width_Plus = -2; width_Plus <= 2; width_Plus++)
            {
                if (brush_Height + height_Plus >= 0 && brush_Height + height_Plus < pixel_Height
                    && brush_Width + width_Plus >= 0 && brush_Width + width_Plus < pixel_Width)
                {
                    pixel_Texture.SetPixel(brush_Width + width_Plus, brush_Height + height_Plus, draw_Color);
                }
            }
        }
        lastPosition = mousePosition_;
    }

    void Draw_Line(Vector2 lastPosition_, Vector2 currentPosition_)
    {
        float distance = Vector2.Distance(currentPosition_, lastPosition_);

        // 간격
        float interval = 1.0f / distance; // 이거를 근데 실수형으로 해도 괜찮나...?

        // 1로 나누는 이유는 두 점 사이를 정확하게 나누고 싶을 때 1을 사용하는 거지

        for (float count = 0.0f; count <= distance; count++)
        {
            Vector2 Line = Vector2.Lerp(lastPosition_, currentPosition_, interval * count);
            // 왜 interval만 사용하면 안 되지...? 선생님한테 질문하기
            // interval만 하면 너무 느리게 나옴
            Draw_Jum(Line);
        }

    }
}
