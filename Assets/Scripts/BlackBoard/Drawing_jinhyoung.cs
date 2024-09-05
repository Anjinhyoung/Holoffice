using UnityEngine;
using UnityEngine.UI;

public class Drawing_jinhyoung : MonoBehaviour
{
    RawImage rawImage;
    
    // Texture2D는 이미지 데이터를 저장하고 조작하는데 사용
    // 특히 내가 사용하는 이유는 이미지 데이터 저장 때문에 
    // 이미지 데이터 저장: 픽셀 단위의 색상 정보를 저장
    // 참고로 float형은 넣을 수 없다. int형만 넣을 수 있다.
    // 또한 인스펙터 컴포넌트에 바로 추가할 수 없다. 왜나하면 컴포넌트가 아닌 Asset 유형(2D 사진)이기 때문에

    Texture2D drawTexture; // 여기서 궁금전 정확히 인스펙터 부분에서는 'Texture'에 해당되는지

    // RawImage의 Rect Transform
    RectTransform rt;

    // 그림판 크기
    int textureSize_width; // 1725
    int textureSize_height; // 935

    public Color backGroundColor;
    public Color drawColor;

    // 마우스의 이전 위치
    Vector2? lastPosition;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
        rawImage = GetComponent<RawImage>();
    }

    void Start()
    {
        textureSize_width = Mathf.RoundToInt(rt.rect.width); // 처음부터 정수로 바꾸기
        textureSize_height = Mathf.RoundToInt(rt.rect.height);
        drawTexture = new Texture2D(textureSize_width, textureSize_height);
        //drawTexture = new Texture2D(1, 1); // 1,1이면  paint rectTransform에 맞춰지니까 커진다.
        // 그 픽셀이 1,1이여도  Rect Transform에 맞춰져서 1, 1이면 
        // Texture2D(1, 1)로 설정하면 1x1 크기의 단일 픽셀 텍스처가 생성

        

        
         // 이 코드는 딱히... 그래도 한 번 선생님한테 물어보기
        for (int y = 0; y < textureSize_height; y++)
        {
            for (int x = 0; x < textureSize_width; x++)
            {
                // 이 코드는 Texture2D를 흰색으로 채우기 위해 모든 픽셀의 색상을 흰색으로 설정하는 초기화 작업
                drawTexture.SetPixel(x, y, backGroundColor);
            }
        }
        drawTexture.Apply(); // 픽셀 데이터를  수정한 후 반드시  Apply()를 호출해야 GPU가 변경사항을 인식하고,

        // 텍스처가 올바르게 업데이트 된다. 이 과정을 거치지 않으면 텍스처의 변경 사항이 화면에 반영되지 않을 수 있다. 

        rawImage.texture = drawTexture; // 텍스처를 UI에 표시하기 위해서는 RawImage에 연결

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 currentPosition;

            // RectTransformUtility.ScreenPointToLocalPointInRectangle()는 
            // 화면상의 스크린 좌표를 특정 UI 요소의 로컬 좌표로 변환해주는 함수이다.

            // 매개 변수로는 로컬 좌표로 변환하고 싶은 UI의 요소, 변환할 스크린 좌표, 변환을 수행할 때 사용할 카메라, 변환된 로컬 좌표가 반환되는 함수
            // 즉 스크린 좌표 => 로컬 좌표 

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rawImage.rectTransform, Input.mousePosition, null, out currentPosition);

            print(currentPosition); // 피봇이 중심이다.

            Vector2 texCoord = new Vector2(
                (currentPosition.x / rawImage.rectTransform.rect.width + 0.5f) * textureSize_width,
                (currentPosition.y / rawImage.rectTransform.rect.height + 0.5f) * textureSize_height
            ); // 한 번 나중에 다시 보기

            if (lastPosition != null)
            {
                DrawSmoothLine(lastPosition.Value, texCoord);
                // 자체 적으로 만든 함수
            }
            else
            {
                DrawPoint(texCoord); // 이것도 자체적으로 만든 함수
            }

            lastPosition = texCoord;
            drawTexture.Apply(); // 항상 그린 걸 바로 바로 변경하기 위해서 그럼 이걸 여기서 하는 게 맞나?
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

        int dx = Mathf.Abs(x1 - x0); // 부호를 무시하고 항상 양수값
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
        int x = Mathf.RoundToInt(position.x); // 처음 그릴 때 
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