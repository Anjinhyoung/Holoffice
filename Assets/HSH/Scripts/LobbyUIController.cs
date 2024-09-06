using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LobbyUIController : MonoBehaviour
{

    public GameObject panel_login;
    public Button btn_login;
    public TMP_InputField input_nickName;
    public GameObject panel_joinOrCreateRoom;
    public static LobbyUIController lobbyUI;
    public TMP_InputField[] roomSetting;


    private void Awake()
    {
        if (lobbyUI == null)
        {
            lobbyUI = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ShowRoomPanel()
    {
        btn_login.interactable = true;
        panel_login.gameObject.SetActive(false);
        panel_joinOrCreateRoom.SetActive(true);
    }
}
