using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if ���࿡ ��Ʈ���� 10���� 10���� canvas�� �ʿ��ϴ�.

    public void OpenNote()
    {
        canvas.gameObject.SetActive(true);
    }
}
