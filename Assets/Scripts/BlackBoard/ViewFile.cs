using UnityEngine;
using UnityEngine.UI;

public class ViewFile : MonoBehaviour
{
    [SerializeField]
    GameObject note;
    [SerializeField]
    GameObject fileDir;


    bool note_active = true;
    bool fileDir_active = false;

    Button file_Button;
    private void Awake()
    {
        file_Button = GetComponent<Button>();   
    }

    public void File_On()
    {
        // ��� ��ư �����(�ڱ� �ڽ� �ٲٱ�)
        note_active = !note_active;
        fileDir_active = !fileDir_active;

        note.SetActive(note_active);
        fileDir.SetActive(fileDir_active);


        ColorBlock change_Button_Color = file_Button.colors;

        // Ȱ��ȭ �� �Ǿ��� ��� ���� �Ͼ��
        if (fileDir_active == false)
        {
            change_Button_Color.selectedColor = Color.white;
            file_Button.colors = change_Button_Color;
        }
        // Ȱ��ȭ �Ǿ��� ��� �ʷϻ�
        else
        {
            change_Button_Color.selectedColor = Color.green;
            file_Button.colors = change_Button_Color;
        }
        

    }
}
