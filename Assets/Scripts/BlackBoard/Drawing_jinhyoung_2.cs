using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;



public class Drawing_jinhyoung_2 : MonoBehaviour
{
    [SerializeField]
    RawImage rawImage; // paint 전체
    RectTransform rt;

    [SerializeField]
    Image blackboard; // 배경색(blackboard) 갖고 오기

    Color backGroundColor; // // 그림판 배경 색깔(blackboard)

    Color drawColor; // 마우스 색갈

    public static Texture2D paint; // int형만 넣을 수 있다. float형은 넣을 수 없다.
    int pixel_Width;  // 픽셀을 채울 크기
    int pixel_Height;

    
    Erase erase;
    public GameObject erase2;

    void Start()
    {
        rt = rawImage.GetComponent<RectTransform>();
        backGroundColor = blackboard.color;
        drawColor = Color.white;
        erase = erase2.GetComponent<Erase>();
        button_pen = GetComponent<Button>();
        button_Erase = erase2.GetComponent<Button>();

        pixel_Width = (int)rt.rect.width;
        pixel_Height = (int)rt.rect.height;

        paint = new Texture2D(pixel_Width, pixel_Height);
        
        for(int height = 0; height < pixel_Height; height++)
        {
            for(int width = 0; width < pixel_Width; width++)
            {
                paint.SetPixel(width, height, backGroundColor); // 픽셀을 blackboard의 색깔로 가져오기 
            }
        }

        paint.Apply();

        rawImage.texture = paint; // 텍스처를 UI에 표시하기 위해서 rawimage에 연결
    }


    // 마우스의 이전 위치
    Vector2 lastPosition;

    void Update()
    {
        if (active)
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 currentPosition;

                // 매개 변수로는 로컬 좌표로 변환하고 싶은 UI의 요소, 변환할 스크린 좌표, 변활을 수행할 때 사용할 카메라, 변환된 로컬 좌표가 반환되는 함수
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    rawImage.rectTransform, Input.mousePosition, null, out currentPosition);

                // print(currentPosition); // 피봇이 중심이다.

                // 정확한 좌표 위치가 필요해서 아래 함수는 필요하다.
                // 0.5f 더하기 전까지는 정규화가 필요하다. 
                // 0.5f를 더해 중심을 기준으로 변환한 후, 이를 곱해 Texture2D의 픽셀 좌표로 변환한다.
                Vector2 mousePosition = new Vector2
                (
                    (currentPosition.x / rawImage.rectTransform.rect.width + 0.5f) * pixel_Width,
                    (currentPosition.y / rawImage.rectTransform.rect.height + 0.5f) * pixel_Height
                );

                if (lastPosition != null && lastPosition != Vector2.zero)
                {
                    Draw_Lerp(lastPosition, mousePosition);
                }
                else
                {
                    Draw(mousePosition);
                }
                lastPosition = mousePosition;
                paint.Apply();
            }
            else
            {
                lastPosition = Vector2.zero;
            }
        }
    }
    
    // 따로 브러쉬 크기 버튼이 없어서 자체적으로 크기를 설정
    void Draw(Vector2 position)
    {
        int brush_Width = (int)position.x;
        int brush_Height = (int)position.y;

        for(int height_Plus = -2; height_Plus <= 2; height_Plus++)
        {
            for(int width_Plus = -2; width_Plus <= 2; width_Plus++)
            {
                if(brush_Height + height_Plus >= 0 && brush_Height + height_Plus < pixel_Height
                    && brush_Width + width_Plus >= 0 && brush_Width + width_Plus < pixel_Width)
                {
                    paint.SetPixel(brush_Width + width_Plus, brush_Height + height_Plus, drawColor);
                }
            }
        }
    }

    void Draw_Lerp(Vector2 lastPosition, Vector2 currentPosition)
    {
        float  dist = Vector2.Distance(currentPosition, lastPosition); 

        if(Mathf.RoundToInt(dist) == 0)
        {
            return;
        }

        float dist_int = 1f / (float)Mathf.RoundToInt(dist); // 1로 나누는 이유는 두 점 사이를 비율로 나누기 위해 (이거 한 번만 더 물어보기)

        // 결론은, 1로 나누는 이유는 딱 그 두 점 사이를 100% 나누기 위해서인 거야. 쉽게 말해, 두 점 사이를 정확하게 나누고 싶을 때 1을 사용하는 거지.

        for (int count = 0; count <= Mathf.RoundToInt(dist); count++)
        {
            Vector2 drowPoint = Vector2.Lerp(lastPosition , currentPosition, dist_int * count);

            Draw(drowPoint);
        }
    }

   
    // 마우스 활성화
    public bool active = false;
    public Button button_pen;
    public void Button_Click()
    {
        active = true;
        erase.active_erase = false;
    }


    public void Button_Change()
    {
        ColorBlock color = button_pen.colors;
        color.normalColor = Color.green;
        button_pen.colors = color;
    }

    Button button_Erase;
    public void Button_Change_erase()
    {
        ColorBlock color = button_Erase.colors;
        color.normalColor = Color.white;
        button_Erase.colors = color;
    }
}
