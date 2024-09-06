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
        // ��Ȱ��ȭ �Ǹ� ���� �ʴ´�.
        text_Collection = GameObject.Find("Text_Collection");
    }
   

    // �׸��� ��ư�� ������ ���
    public void To_Paint()
    {
        // �׸��� ������ ���̰�  �ϰ�
        paint_Collection.SetActive(true);

        // �ؽ�Ʈ ������ �� ���̰� �ϰ�
        text_Collection.SetActive(false);

        // �׸��� ��ư�� �� ���̰� �ϰ�
        gameObject.SetActive(false);

        // ������ �ؽ�Ʈ ��ư�� ���̰� �ϰ�
        text_Button.SetActive(true);

    }
}
