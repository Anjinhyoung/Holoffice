using UnityEngine;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    public RawImage rawImage;
    
    // Texture2D는 이미지 데이터를 저장하고 조작하는데 사용
    // 특히 내가 사용하는 이유는 이미지 데이터 저장 때문에 
    // 이미지 데이터 저장: 픽셀 단위의 색상 정보를 저장
    private Texture2D drawTexture;

    // 
    public int textureSize = 512;
    public Color drawColor = Color.black;
    private Vector2? lastPosition;

    void Start()
    {
        drawTexture = new Texture2D(textureSize, textureSize);
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                drawTexture.SetPixel(x, y, Color.white);
            }
        }
        drawTexture.Apply();
        rawImage.texture = drawTexture;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform, Input.mousePosition, null, out currentPosition);

            // 텍스처 좌표로 변환
            Vector2 texCoord = new Vector2(
                (currentPosition.x / rawImage.rectTransform.rect.width + 0.5f) * textureSize,
                (currentPosition.y / rawImage.rectTransform.rect.height + 0.5f) * textureSize
            );

            if (lastPosition.HasValue)
            {
                DrawSmoothLine(lastPosition.Value, texCoord);
            }
            else
            {
                DrawPoint(texCoord);
            }

            lastPosition = texCoord;
            drawTexture.Apply();
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

        int dx = Mathf.Abs(x1 - x0);
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
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                if (x + i >= 0 && x + i < textureSize && y + j >= 0 && y + j < textureSize)
                {
                    drawTexture.SetPixel(x + i, y + j, drawColor);
                }
            }
        }
    }
}