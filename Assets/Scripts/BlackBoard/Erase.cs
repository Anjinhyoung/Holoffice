using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    RawImage background; // blackboard(���찳 ���� ���ҷ���)

    [SerializeField]
    Color erase_color; // ���찳 ����

    [SerializeField]
    RawImage paint; // �׸���

    RectTransform rt; // �׸��� ũ��

    private void Awake()
    {
        background = GameObject.FindWithTag("blackboard").GetComponent<RawImage>();
        rt = paint.GetComponent<RectTransform>();
    }

    private void Start()
    {
        erase_color = background.color;
    }

    // ���� ��ǥ
    Vector2 lastPosition;
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            // ���� ��ǥ
            Vector2 currentPosition;

            // ��ũ�� ��ǥ�� ȭ�� ��ǥ�� �ٲٱ�
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rt, Input.mousePosition, null, out currentPosition); // ������ ������� �ϴϱ�

            // currentPosition�� ������ 0.5, 0.5 �̱� ������
            // �� ���� �Ʒ� ���� (0, 0)���� �� �� �� ������� �Ѵ�.
            Vector2 mousePosition = new Vector2(
            (currentPosition.x / rt.rect.width + 0.5f) * rt.rect.width,
            (currentPosition.y / rt.rect.height + 0.5f) * rt.rect.height
            );


            if (lastPosition != null && lastPosition != Vector2.zero) // ���� ������ ���� ��� ��� ���� but ���� ������ �Ұ���
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
