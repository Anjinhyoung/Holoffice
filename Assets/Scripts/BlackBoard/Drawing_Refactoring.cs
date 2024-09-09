using UnityEngine;
using UnityEngine.UI;

public class Drawing_Refactoring : MonoBehaviour
{
    [SerializeField]
    Image blackboard; // blackboard (�������� ������ blackboard�� ������ paint�� ��ĥ�ϱ� ���ؼ�)

    [SerializeField]
    RawImage paint; // �׸���
    RectTransform paint_RT; // �׸��� ũ��
    Color paint_Color; // blackboard�� ������ paint�� ��ĥ�ϱ� ���ؼ�

    public static Color draw_Color = Color.white; // �� ���� (public static ���� ������ ������ 'ȭ��Ʈ ���� ��ȯ' ������ �����ϱ� ���ؼ�)

    public static Texture2D pixel_Texture; // public static ���� ������ ������ '����'�� �ϱ� ���ؼ�(Erase Script�� ����)
    int pixel_Width, pixel_Height;

    private void Awake() // ���� �� �ʱ�ȭ ���⼭ �� �� ������� �Ѵ�. (���� ������)
    {

        paint_RT = paint.GetComponent<RectTransform>();
        paint_Color = blackboard.color; // ������ �� ���� ���������� �����ϱ�!

        pixel_Width = (int)paint_RT.rect.width;
        pixel_Height = (int)paint_RT.rect.height;

        pixel_Texture = new Texture2D(pixel_Width, pixel_Height);

        for (int height = 0; height < pixel_Height; height++)
        {
            for (int width = 0; width < pixel_Width; width++)
            {
                pixel_Texture.SetPixel(width, height, paint_Color); // for�� �ٽ� �� �� ���������� �����ѱ�
            }
        }

        pixel_Texture.Apply();

        paint.texture = pixel_Texture; // Apply()���� �� �� Texture�� �׸��� Texture�� ����

        pen_Button = GetComponent<Button>();
    }

    public static bool pen_Active = false; // public static���� ������ ������ Erase �Լ������� ����ؾ� �ϱ� ������
    public static Button pen_Button; // public���� ������ ������ Erase �Լ������� ����ؾ� �ϱ� ������

    public void Button_On()
    {
        pen_Active = true;

        // pen ��ư�� Ȱ��ȭ �Ǿ��� ��� pen ��ư ���� ������� �ٲٱ�
        ColorBlock change_Button_Color = pen_Button.colors;
        change_Button_Color.normalColor = Color.green;
        pen_Button.colors = change_Button_Color;

        // ���찳 ��ư �������� �ٲٰ� Ȱ��ȭ�ϱ�
        Erase_Refactoring.erase_Active = false;
        Erase_Refactoring.Button_Off();
    }

    public static void Button_Off()
    {
        // ���찳 ��ư�� Ȱ��ȭ �Ǿ��� ��� pen ��ư ���� �Ͼ������ �ٲٱ�
        ColorBlock change_Button_Color = pen_Button.colors;
        change_Button_Color.normalColor = Color.white;
        pen_Button.colors = change_Button_Color;
    }


    Vector2 lastPosition, currentPosition = Vector2.zero; // ���콺 ���� ��ġ, ���� ��ġ (�ʱⰪ�� ����)
    Vector2 mousePosition; // ScreenPointToLocalPointInRectangle() �� �Լ��� �ٲ� ���콺 ��ǥ

    void Update()
    {
        // ��ư�� �������� ���
        if (pen_Active)
        {
            // �Ű� �����δ� ���� ��ǥ�� ��ȯ�ϰ� ���� UI ���, ��ȯ�� ��ũ�� ��ǥ, ��ȯ�� ������ �� ����� ī�޶�, ��ȯ�� ���� ��ǥ ���� ���� ����
            RectTransformUtility.ScreenPointToLocalPointInRectangle(paint_RT, Input.mousePosition, null, out currentPosition);

            // print(currentPosition) �Ǻ��� �߽����� ���´�.

            mousePosition = new Vector2
            (
                (currentPosition.x / pixel_Width + 0.5f) * pixel_Width,
                (currentPosition.y / pixel_Height + 0.5f) * pixel_Height
            ); // �̰Ÿ� �� �ٲ��ִ� ������ ��Ȯ�� �ڱⰡ ���콺 ���������� ���ϰ� �׸��� ���ؼ�...? �̰͵� ������ ���� ����

            if (Input.GetMouseButtonDown(0)) // ���콺 �� �� ���� �������� ��
            {
                // �� �ϳ��� �� ���� �� �ְ� �ϱ�
                Draw_Jum(mousePosition);
            }

            else if (Input.GetMouseButton(0)) // ���콺�� ��� ������ ���� ��
            {
                // ����� �׷��� �� �ְ�
                Draw_Line(lastPosition, mousePosition);
            }

            pixel_Texture.Apply();
        }
    }


    void Draw_Jum(Vector2 mousePosition_)
    {
        // �̰ͺ��� �� ���� �� ������? ���������� �����ϱ�

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

        // ����
        float interval = 1.0f / distance; // �̰Ÿ� �ٵ� �Ǽ������� �ص� ������...?

        // 1�� ������ ������ �� �� ���̸� ��Ȯ�ϰ� ������ ���� �� 1�� ����ϴ� ����

        for (float count = 0.0f; count <= distance; count++)
        {
            Vector2 Line = Vector2.Lerp(lastPosition_, currentPosition_, interval * count);
            // �� interval�� ����ϸ� �� ����...? ���������� �����ϱ�
            // interval�� �ϸ� �ʹ� ������ ����
            Draw_Jum(Line);
        }

    }
}
