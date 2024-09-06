using UnityEngine;
using UnityEngine.UI;

public class TextToPaint : MonoBehaviour
{
    [SerializeField]
    GameObject paint_Collection;
    [SerializeField]
    GameObject text_Button;

    GameObject text_Collection;

    private void Awake()
    {
        // 비활성화 되면 뜨지 않는다.
        text_Collection = GameObject.Find("Text_Collection");
    }
   

    // 그림판 버튼을 눌렀을 경우
    public void To_Paint()
    {
        // 그림판 모음집 보이게  하고
        paint_Collection.SetActive(true);

        // 텍스트 모음집 안 보이게 하고
        text_Collection.SetActive(false);

        // 그림판 버튼도 안 보이게 하고
        gameObject.SetActive(false);

        // 마지막 텍스트 버튼이 보이게 하고
        text_Button.SetActive(true);

    }
}
