using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if ���࿡ ��Ʈ���� 10���� 10���� canvas�� �ʿ��ϴ�.


    private void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            OpenNote();
        }
    }

    public void OpenNote()
    {
        canvas.gameObject.SetActive(true);
    }
}
