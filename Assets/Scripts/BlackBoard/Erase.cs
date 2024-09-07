using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    RawImage background; // blackboard(지우개 색깔 구할려고)

    [SerializeField]
    Color erase_color; // 지우개 색깔

    [SerializeField]
    RawImage paint; // 그림판

    RectTransform rt; // 그림판 크기

    private void Awake()
    {
        background = GameObject.FindWithTag("blackboard").GetComponent<RawImage>();
        rt = paint.GetComponent<RectTransform>();
    }

    private void Start()
    {
        erase_color = background.color;
    }

    // 이전 좌표
    Vector2 lastPosition;
    void Update()
    {

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

    Texture2D pixel_Paint;
    void Erase_(Vector2 mousePosition)
    {
        pixel_Paint = paint.GetComponent<Texture2D>();

        int brush_Width = (int)mousePosition.x;
        int brush_Height = (int)mousePosition.y;

        for(int height_Plus = -2; height_Plus <= 2; height_Plus++)
        {
            for(int width_Plus = -2; width_Plus <= 2; width_Plus++)
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
       
    }
}
