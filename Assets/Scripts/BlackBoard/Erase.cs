using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class Erase : MonoBehaviour
{
    // blackboard(���찳 ���� ���ҷ���)
    RawImage background;

    // ���찳 ����
    [SerializeField]
    Color erase_color;

    // �׸���
    [SerializeField]
    RawImage paint;

    // �׸��� ũ��
    RectTransform rt;

    // �׸��� ������ �ȼ� ũ��
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

    // ���� ��ǥ
    Vector2 lastPosition;
    void Update()
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
        
        if(lastPosition != null) // ���� ������ ���� ��� ��� ���� but ���� ������ �Ұ���
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
        // pixel_Paint.Apply(); // Apply()�� �ʿ��Ѱ�?
        // paint.texture = pixel_Paint; �̰� �ʿ��Ѱ�?




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
