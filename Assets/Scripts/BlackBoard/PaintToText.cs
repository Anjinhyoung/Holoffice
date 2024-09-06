using UnityEngine;
using UnityEngine.UI;

public class PaintToText : MonoBehaviour
{
    [SerializeField]
    GameObject text_Collection;
    [SerializeField]
    GameObject paint_Button;
    GameObject paint_Collection;

    // �ؽ�Ʈ ��ư�� ������ ���

    public void To_Text()
    {
        
        paint_Collection = GameObject.Find("Paint_Collection");

        // �׸��� ������ ��Ȱ��ȭ
        paint_Collection.SetActive(false);
        // �ؽ�Ʈ ������ Ȱ��ȭ
        text_Collection.SetActive(true);
        // �ؽ�Ʈ ��ư ��Ȱ��ȭ
        gameObject.SetActive(false);
        // �׸��� ��ư Ȱ��ȭ
        paint_Button.SetActive(true);
    }
}