using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextToPaint : MonoBehaviour
{
    public GameObject PaintToText_Button;
    public GameObject note;
    public GameObject paint;

    public GameObject pen;
    public GameObject brush;
    public GameObject erase;


    public GameObject language;
    public GameObject language2;
    public GameObject language3;

    public GameObject ai;

    // UI 버튼을 눌렀을 경우
    public void To_Paint()
    {
        // 자기 버튼 비활성화
        gameObject.SetActive(false);

        // TEXT 버튼 활성화
        PaintToText_Button.SetActive(true);

        // 노트 비활성화
        note.SetActive(false);

        // 그림판 활성화
        paint.SetActive(true);

        // 펜 활성화
        pen.SetActive(true);

        // 브러쉬 활성화
        brush.SetActive(true);

        // 지우개 활성화
        erase.SetActive(true);

        // Ai 비활성화
        ai.SetActive(false);


        // language 관련 버튼들 비활성화
        language.SetActive(false);
        language2.SetActive(false);
        language3.SetActive(false);
    }


}
