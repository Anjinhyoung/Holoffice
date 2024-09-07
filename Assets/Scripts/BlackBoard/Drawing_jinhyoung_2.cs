using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;


public class Drawing_jinhyoung_2 : MonoBehaviour
{
    RawImage rawImage; // paint ��ü
    RectTransform rt;

    [SerializeField]
    Image blackboard; // ����(blackboard) ���� ����

    Color backGroundColor; // // �׸��� ��� ����(blackboard)

    Color drawColor; // ���콺 ����

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
        rt = rawImage.GetComponent<RectTransform>();
        backGroundColor = blackboard.color;
        drawColor = Color.white;
    }

    Texture2D paint; // int���� ���� �� �ִ�. float���� ���� �� ����.
    int pixel_Width;  // �ȼ��� ä�� ũ��
    int pixel_Height;

    void Start()
    {
        pixel_Width = (int)rt.rect.width;
        pixel_Height = (int)rt.rect.height;

        paint = new Texture2D(pixel_Width, pixel_Height);
        
        for(int height = 0; height < pixel_Height; height++)
        {
            for(int width = 0; width < pixel_Width; width++)
            {
                paint.SetPixel(width, height, backGroundColor); // �ȼ��� blackboard�� ����� �������� 
            }
        }

        paint.Apply();

        rawImage.texture = paint; // �ؽ�ó�� UI�� ǥ���ϱ� ���ؼ� rawimage�� ����
    }


    // ���콺�� ���� ��ġ
    Vector2 lastPosition;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition;

            // �Ű� �����δ� ���� ��ǥ�� ��ȯ�ϰ� ���� UI�� ���, ��ȯ�� ��ũ�� ��ǥ, ��Ȱ�� ������ �� ����� ī�޶�, ��ȯ�� ���� ��ǥ�� ��ȯ�Ǵ� �Լ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform, Input.mousePosition, null, out currentPosition); 

            // print(currentPosition); // �Ǻ��� �߽��̴�.

            // ��Ȯ�� ��ǥ ��ġ�� �ʿ��ؼ� �Ʒ� �Լ��� �ʿ��ϴ�.
            // 0.5f ���ϱ� �������� ����ȭ�� �ʿ��ϴ�. 
            // 0.5f�� ���� �߽��� �������� ��ȯ�� ��, �̸� ���� Texture2D�� �ȼ� ��ǥ�� ��ȯ�Ѵ�.
            Vector2 mousePosition = new Vector2
            (
                (currentPosition.x / rawImage.rectTransform.rect.width + 0.5f) * pixel_Width,
                (currentPosition.y / rawImage.rectTransform.rect.height + 0.5f) * pixel_Height
            );

            if(lastPosition != null && lastPosition != Vector2.zero)
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
    
    // ���� �귯�� ũ�� ��ư�� ��� ��ü������ ũ�⸦ ����
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

        float dist_int = 1f / (float)Mathf.RoundToInt(dist);

        //Vector2 fillPosition = Vector2.Lerp(currentPosition,lastPosition, dist_int);

        for(int count = 0; count <= Mathf.RoundToInt(dist); count++)
        {
            Vector2 drowPoint = Vector2.Lerp(lastPosition , currentPosition, dist_int * count);

            Draw(drowPoint);
        }

        // print(Mathf.RoundToInt(dist) + dist_int);
    }
}
