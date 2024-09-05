using UnityEngine;
using UnityEngine.UI;

public class Drawing_jinhyoung : MonoBehaviour
{
    RawImage rawImage;
    
    // Texture2D�� �̹��� �����͸� �����ϰ� �����ϴµ� ���
    // Ư�� ���� ����ϴ� ������ �̹��� ������ ���� ������ 
    // �̹��� ������ ����: �ȼ� ������ ���� ������ ����
    // ����� float���� ���� �� ����. int���� ���� �� �ִ�.
    // ���� �ν����� ������Ʈ�� �ٷ� �߰��� �� ����. �ֳ��ϸ� ������Ʈ�� �ƴ� Asset ����(2D ����)�̱� ������

    Texture2D drawTexture; // ���⼭ �ñ��� ��Ȯ�� �ν����� �κп����� 'Texture'�� �ش�Ǵ���

    // RawImage�� Rect Transform
    RectTransform rt;

    // �׸��� ũ��
    int textureSize_width; // 1725
    int textureSize_height; // 935

    public Color backGroundColor;
    public Color drawColor;

    // ���콺�� ���� ��ġ
    Vector2? lastPosition;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        rawImage = GetComponent<RawImage>();
    }

    void Start()
    {
        textureSize_width = Mathf.RoundToInt(rt.rect.width); // ó������ ������ �ٲٱ�
        textureSize_height = Mathf.RoundToInt(rt.rect.height);
        drawTexture = new Texture2D(textureSize_width, textureSize_height);
        //drawTexture = new Texture2D(1, 1); // 1,1�̸�  paint rectTransform�� �������ϱ� Ŀ����.
        // �� �ȼ��� 1,1�̿���  Rect Transform�� �������� 1, 1�̸� 
        // Texture2D(1, 1)�� �����ϸ� 1x1 ũ���� ���� �ȼ� �ؽ�ó�� ����

        

        
         // �� �ڵ�� ����... �׷��� �� �� ���������� �����
        for (int y = 0; y < textureSize_height; y++)
        {
            for (int x = 0; x < textureSize_width; x++)
            {
                // �� �ڵ�� Texture2D�� ������� ä��� ���� ��� �ȼ��� ������ ������� �����ϴ� �ʱ�ȭ �۾�
                drawTexture.SetPixel(x, y, backGroundColor);
            }
        }
        drawTexture.Apply(); // �ȼ� �����͸�  ������ �� �ݵ��  Apply()�� ȣ���ؾ� GPU�� ��������� �ν��ϰ�,

        // �ؽ�ó�� �ùٸ��� ������Ʈ �ȴ�. �� ������ ��ġ�� ������ �ؽ�ó�� ���� ������ ȭ�鿡 �ݿ����� ���� �� �ִ�. 

        rawImage.texture = drawTexture; // �ؽ�ó�� UI�� ǥ���ϱ� ���ؼ��� RawImage�� ����

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition;

            // RectTransformUtility.ScreenPointToLocalPointInRectangle()�� 
            // ȭ����� ��ũ�� ��ǥ�� Ư�� UI ����� ���� ��ǥ�� ��ȯ���ִ� �Լ��̴�.

            // �Ű� �����δ� ���� ��ǥ�� ��ȯ�ϰ� ���� UI�� ���, ��ȯ�� ��ũ�� ��ǥ, ��ȯ�� ������ �� ����� ī�޶�, ��ȯ�� ���� ��ǥ�� ��ȯ�Ǵ� �Լ�
            // �� ��ũ�� ��ǥ => ���� ��ǥ 

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform, Input.mousePosition, null, out currentPosition);

            print(currentPosition); // �Ǻ��� �߽��̴�.

            Vector2 texCoord = new Vector2(
                (currentPosition.x / rawImage.rectTransform.rect.width + 0.5f) * textureSize_width,
                (currentPosition.y / rawImage.rectTransform.rect.height + 0.5f) * textureSize_height
            ); // �� �� ���߿� �ٽ� ����

            if (lastPosition != null)
            {
                DrawSmoothLine(lastPosition.Value, texCoord);
                // ��ü ������ ���� �Լ�
            }
            else
            {
                DrawPoint(texCoord); // �̰͵� ��ü������ ���� �Լ�
            }

            lastPosition = texCoord;
            drawTexture.Apply(); // �׻� �׸� �� �ٷ� �ٷ� �����ϱ� ���ؼ� �׷� �̰� ���⼭ �ϴ� �� �³�?
        }
        else if (Input.GetMouseButtonUp(0))
        {
            lastPosition = null;
        }
    }

    void DrawSmoothLine(Vector2 start, Vector2 end)
    {
        int x0 = Mathf.RoundToInt(start.x);
        int y0 = Mathf.RoundToInt(start.y);
        int x1 = Mathf.RoundToInt(end.x);
        int y1 = Mathf.RoundToInt(end.y);

        int dx = Mathf.Abs(x1 - x0); // ��ȣ�� �����ϰ� �׻� �����
        int dy = Mathf.Abs(y1 - y0);
        int sx = x0 < x1 ? 1 : -1;
        int sy = y0 < y1 ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            DrawPoint(new Vector2(x0, y0));

            if (x0 == x1 && y0 == y1) break;
            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x0 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y0 += sy;
            }
        }
    }

    void DrawPoint(Vector2 position)
    {
        int x = Mathf.RoundToInt(position.x); // ó�� �׸� �� 
        int y = Mathf.RoundToInt(position.y);

        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (x + i >= 0 && x + i < textureSize_width && y + j >= 0 && y + j < textureSize_height)
                {
                    drawTexture.SetPixel(x + i, y + j, drawColor);
                }
            }
        }

    }
}