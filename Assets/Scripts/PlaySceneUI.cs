using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneUI : MonoBehaviour
{
    public Button btn_Chat;
    public Button btn_Exit;
    public GameObject panel_Chat;
    public Button btn_InputChat;

    bool chatOpen = false;

    void Start()
    {
        btn_Chat.onClick.AddListener(OpenChat);
        btn_Exit.onClick.AddListener(Exit);
        btn_InputChat.onClick.AddListener(InputChat);
    }

    void OpenChat()
    {
        panel_Chat.SetActive(!chatOpen);
        chatOpen = !chatOpen;
    }

    void Exit()
    {

    }

    void InputChat()
    {

    }

}
