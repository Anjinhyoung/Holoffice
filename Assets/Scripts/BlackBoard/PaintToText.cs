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

    // UI ��ư�� ������ ���
    public void To_Text()
    {
        gameObject.SetActive(false);
        TextToPaint_Button.SetActive(true);
        paint.SetActive(false);
        Note.SetActive(true);


        // �� ��Ȱ��ȭ
        pen.SetActive(false);

        // �귯�� ��Ȱ��ȭ
        brush.SetActive(false);

        // ���찳 ��Ȱ��ȭ
        erase.SetActive(false);

        // Ai Ȱ��ȭ
        ai.SetActive(true);


        // language ���� ��ư�� Ȱ��ȭ
        language.SetActive(true);
        language2.SetActive(true);
        language3.SetActive(true);
    }
}