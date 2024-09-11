using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if 만약에 노트북이 10대라면 10개의 canvas가 필요하다.

    public void OpenNote()
    {
        canvas.gameObject.SetActive(true);
    }
}
