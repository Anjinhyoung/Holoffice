using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    [SerializeField]
    Image background; // blackboard(지우개 색깔 구할려고)

    Color erase_color; // 지우개 색깔

    [SerializeField]
    RawImage paint; // 그림판

    RectTransform rt; // 그림판 크기

    // 이전 좌표
    Vector2 lastPosition;

    Drawing_jinhyoung_2 dj;
    public GameObject pen;


    public Button button;
    private void Start()
    {
        rt = paint.GetComponent<RectTransform>();
        dj = pen.GetComponent<Drawing_jinhyoung_2>();
        button = GetComponent<Button>();
        pen_Button = pen.GetComponent<Button>();
    }
    void Update()
    {
        // 지우개 버튼 눌렀을 때만 활성화
        if (active_erase)
        {
            Erase_Button();

            if (Input.GetMouseButton(0))
            {
                // 현재 좌표
                Vector2 currentPosition;

                // 스크린 좌표를 화면 좌표로 바꾸기
                RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out currentPosition); // 범위를 정해줘야 하니까

                // currentPosition은 중점이 0.5, 0.5 이기 때문에
                // 맨 왼쪽 아래 부터 (0, 0)으로 한 번 더 맞춰줘야 한다.
                Vector2 mousePosition = new Vector2(
                (currentPosition.x / rt.rect.width + 0.5f) * rt.rect.width,
                (currentPosition.y / rt.rect.height + 0.5f) * rt.rect.height
                );


                if (lastPosition != null && lastPosition != Vector2.zero) // 전역 변수는 값이 없어도 사용 가능 but 지역 변수는 불가능
                {
                    Erase_Lerp(lastPosition, mousePosition);
                }
                else
                {
                    Erase_(mousePosition);
                }
                lastPosition = mousePosition;
                pixel_Paint.Apply();
            }
            else
            {
                lastPosition = Vector2.zero;
            }
        }
    }

    Texture2D pixel_Paint;
    void Erase_(Vector2 mousePosition)
    {
        int brush_Width = (int)mousePosition.x;
        int brush_Height = (int)mousePosition.y;

        for(int height_Plus = -10; height_Plus <= 10; height_Plus++)
        {
            for(int width_Plus = -10; width_Plus <= 10; width_Plus++)
            {
                if(brush_Height + height_Plus >=0 && brush_Height + height_Plus < rt.rect.height &&
                    brush_Width + width_Plus >=0 && brush_Width + width_Plus < rt.rect.width)
                {
                    pixel_Paint.SetPixel(brush_Width + width_Plus, brush_Height + height_Plus, erase_color);
                }
            }
        }
    }

    void Erase_Lerp(Vector2 lastPosition, Vector2 currentPosition)
    {
        float distance = Vector2.Distance(currentPosition, lastPosition);

        if(Mathf.RoundToInt(distance) == 0)
        {
            return;
        }

        float dist_int = 1.0f / (float)Mathf.RoundToInt(distance); // 1로 나누는 이유는 두 점 사이를 비율로 나누기 위해

        for(int count = 0;  count <= Mathf.RoundToInt(distance); count++)
        {
            Vector2 erasePoint = Vector2.Lerp(lastPosition, currentPosition, dist_int * count);
            Erase_(erasePoint);
        }
    }
    public bool active_erase = false;
    
    public void Erase_Button()
    {
        erase_color = background.color;
        pixel_Paint = Drawing_jinhyoung_2.paint;


        active_erase = true;
        dj.active = false;
    }

    public void Button_Change()
    {
        // 버튼이 활성화 되어있는 동안 색깔 바꾸는 법 => 원래 이렇게 귀찮나...?
        ColorBlock color = button.colors;
        color.normalColor = Color.green;
        button.colors = color;
    }

    Button pen_Button;
    public void Button_Change_pen()
    {
        ColorBlock color = pen_Button.colors;
        color.normalColor = Color.white;
        pen_Button.colors = color;
    }
}
