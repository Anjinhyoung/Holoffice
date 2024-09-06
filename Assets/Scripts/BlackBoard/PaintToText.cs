using UnityEngine;
using UnityEngine.UI;

public class PaintToText : MonoBehaviour
{
    [SerializeField]
    GameObject text_Collection;
    [SerializeField]
    GameObject paint_Button;
    GameObject paint_Collection;

    // 텍스트 버튼을 눌렀을 경우

    public void To_Text()
    {
        
        paint_Collection = GameObject.Find("Paint_Collection");

        // 그림판 모음집 비활성화
        paint_Collection.SetActive(false);
        // 텍스트 모음집 활성화
        text_Collection.SetActive(true);
        // 텍스트 버튼 비활성화
        gameObject.SetActive(false);
        // 그림판 버튼 활성화
        paint_Button.SetActive(true);
    }
}