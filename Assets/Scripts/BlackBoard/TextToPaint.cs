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

    // UI ��ư�� ������ ���
    public void To_Paint()
    {
        // �ڱ� ��ư ��Ȱ��ȭ
        gameObject.SetActive(false);

        // TEXT ��ư Ȱ��ȭ
        PaintToText_Button.SetActive(true);

        // ��Ʈ ��Ȱ��ȭ
        note.SetActive(false);

        // �׸��� Ȱ��ȭ
        paint.SetActive(true);

        // �� Ȱ��ȭ
        pen.SetActive(true);

        // �귯�� Ȱ��ȭ
        brush.SetActive(true);

        // ���찳 Ȱ��ȭ
        erase.SetActive(true);

        // Ai ��Ȱ��ȭ
        ai.SetActive(false);


        // language ���� ��ư�� ��Ȱ��ȭ
        language.SetActive(false);
        language2.SetActive(false);
        language3.SetActive(false);
    }


}
