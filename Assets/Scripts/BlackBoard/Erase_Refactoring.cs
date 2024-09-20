using UnityEngine;
using UnityEngine.UI;

public class Erase_Refactoring : MonoBehaviour
{
    [SerializeField]
    Image blackboard; // blackboard(���찳 ���� ���ҷ���)

    [SerializeField]
    RawImage paint; // �׸���
    RectTransform paint_RT; // �׸��� ũ��

    int pixel_Width, pixel_Height;

    Color erase_Color; // ���찳 ����(blackboard�� ������ �޴´�.)

    void Awake()
    {
        paint_RT = paint.GetComponent<RectTransform>();
        pixel_Width = (int)paint_RT.rect.width;
        pixel_Height = (int)paint_RT.rect.height;
        // ���ڱ� static�� �׳� ���� ���� ������ �� ���ڱ� �ñ����� �̰͵� ������ ���� �����غ���

        erase_Color = blackboard.color;

        erase_Button = GetComponent<Button>();
    }

    public static bool erase_Active = false;
    public static Button erase_Button;
    public void Button_On()
    {
        erase_Active = true;

        // ���찳 ��ư�� Ȱ��ȭ �Ǿ��� ��� ���찳 ��ư ���� ������� �ٲٱ�
        ColorBlock change_Button_Color = erase_Button.colors;
        change_Button_Color.normalColor = Color.green;
        erase_Button.colors = change_Button_Color;

        // pen ��ư �Ͼ������ �ٲٰ� ��Ȱ��ȭ�ϱ�
        Drawing_Refactoring.pen_Active = false;
        Drawing_Refactoring.Button_Off();

        // ������ ��ư �Ͼ������ �ٲٰ� ��Ȱ��ȭ�ϱ�
        trash.trash_Active = false;
        trash.Button_Off();
    }

    public static void Button_Off()
    {
        // pen ��ư�� Ȱ��ȭ �Ǿ��� ��� ���찳 ��ư ���� �Ͼ������ �ٲٱ�
        ColorBlock change_Button_Color = erase_Button.colors;
        change_Button_Color.normalColor = Color.white;
        erase_Button.colors = change_Button_Color;
    }

    Vector2 lastPosition, currentPosition = Vector2.zero; // ���콺 ���� ��ġ, ���� ��ġ (�ʱⰪ�� ����)
    Vector2 mousePosition; // ScreenPointToLocalPointInRectangle() �� �Լ��� �ٲ� ���콺 ��ǥ


    void Update()
    {
        // ���찳 ��ư ������ ���� Ȱ��ȭ
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
                // �� ��ư �� ���� ������ ���(�� �ϳ��� ���� ���)
                Erase_Jum(mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                // ��ư�� ��� ������ ���(�������� ������ ���)
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

        // ����
        float interval = 1.0f / distance; // �̰Ÿ� �ٵ� �Ǽ������� �ص� ������...?

        // 1�� ������ ������ �� �� ���̸� ��Ȯ�ϰ� ������ ���� �� 1�� ����ϴ� ����

        for (float count = 0.0f; count <= distance; count++)
        {
            Vector2 Line = Vector2.Lerp(lastPosition_, currentPosition_, interval * count);
            Erase_Jum(Line);
        }
    }
}
