using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    [SerializeField]
    Image background; // blackboard(���찳 ���� ���ҷ���)

    Color erase_color; // ���찳 ����

    [SerializeField]
    RawImage paint; // �׸���

    RectTransform rt; // �׸��� ũ��

    // ���� ��ǥ
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
        // ���찳 ��ư ������ ���� Ȱ��ȭ
        if (active_erase)
        {
            Erase_Button();

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

        float dist_int = 1.0f / (float)Mathf.RoundToInt(distance); // 1�� ������ ������ �� �� ���̸� ������ ������ ����

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
        // ��ư�� Ȱ��ȭ �Ǿ��ִ� ���� ���� �ٲٴ� �� => ���� �̷��� ������...?
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
