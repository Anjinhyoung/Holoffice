using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PaintToText : MonoBehaviour
{
    public GameObject TextToPaint_Button;
    public GameObject paint;
    public GameObject Note;

    public GameObject pen;
    public GameObject brush;
    public GameObject erase;


    public GameObject language;
    public GameObject language2;
    public GameObject language3;

    public GameObject ai;

    // UI 버튼을 눌렀을 경우
    public void To_Text()
    {
        gameObject.SetActive(false);
        TextToPaint_Button.SetActive(true);
        paint.SetActive(false);
        Note.SetActive(true);


        // 펜 비활성화
        pen.SetActive(false);

        // 브러쉬 비활성화
        brush.SetActive(false);

        // 지우개 비활성화
        erase.SetActive(false);

        // Ai 활성화
        ai.SetActive(true);


        // language 관련 버튼들 활성화
        language.SetActive(true);
        language2.SetActive(true);
        language3.SetActive(true);
    }
}