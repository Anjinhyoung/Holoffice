using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    // blackboard(지우개 색깔 구할려고)
    RawImage background;

    // 지우개 색깔
    [SerializeField]
    Color erase_color;

    // 그림판
    [SerializeField]
    RawImage paint;

    // 그림판 크기
    RectTransform rt;

    // 그림판 내에서 픽셀 크기
    Texture2D pixel_Paint;
    

    private void Awake()
    {
        background = GameObject.Find("blackboard").GetComponent<RawImage>();
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
        
        if(lastPosition != null) // 전역 변수는 값이 없어도 사용 가능 but 지역 변수는 불가능
        {

        }
        else
        {
            EraseJum(mousePosition);
        }
        
    }




    public void EraseJum(Vector2 mousePosition)
    {
        pixel_Paint = paint.GetComponent<Texture2D>();
        // pixel_Paint.Apply(); // Apply()가 필요한가?
        // paint.texture = pixel_Paint; 이게 필요한가?




        for(int height_Plus = -2; height_Plus <= 2; height_Plus++)
        {
            for(int width_Plus = -2; width_Plus <= 2; width_Plus++)
            {
                if((int)mousePosition.y + height_Plus >=0 && (int)mousePosition.y + height_Plus < rt.rect.height &&
                    (int)mousePosition.x + width_Plus >=0 && (int)mousePosition.x + width_Plus < rt.rect.width)
                {
                    pixel_Paint.SetPixel((int)mousePosition.x, (int)mousePosition.y, erase_color);
                }
            }
        }
    }
}
