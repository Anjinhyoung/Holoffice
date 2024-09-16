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
        // 토글 버튼 만들기(자기 자신 바꾸기)
        note_active = !note_active;
        fileDir_active = !fileDir_active;

        note.SetActive(note_active);
        fileDir.SetActive(fileDir_active);


        ColorBlock change_Button_Color = file_Button.colors;

        // 활성화 안 되었을 경우 원래 하얀색
        if (fileDir_active == false)
        {
            change_Button_Color.selectedColor = Color.white;
            file_Button.colors = change_Button_Color;
        }
        // 활성화 되었을 경우 초록색
        else
        {
            change_Button_Color.selectedColor = Color.green;
            file_Button.colors = change_Button_Color;
        }
        

    }
}
