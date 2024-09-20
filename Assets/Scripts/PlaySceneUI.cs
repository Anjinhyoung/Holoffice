using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;

public class PlaySceneUI : MonoBehaviourPun, IOnEventCallback
{
    public Button btn_Chat;
    public Button btn_Exit;
    public GameObject panel_Chat;
    public Button btn_InputChat;
    public TMP_Text text_chat;
    public ScrollRect scroll_Chat;
    public TMP_InputField input_Chat; 

    bool chatOpen = false;

    const byte chattingEvent = 1;

    void Start()
    {
        input_Chat.text = "";

        btn_Chat.onClick.AddListener(OpenChat);
        btn_Exit.onClick.AddListener(Exit);
        btn_InputChat.onClick.AddListener(InputChat);

        input_Chat.onSubmit.AddListener(SendMyMessage);

    }

    void SendMyMessage(string msg)
    {
        object[] sendContent = new object[] { PhotonNetwork.NickName, msg };
        RaiseEventOptions eventOptions = new RaiseEventOptions();
        eventOptions.Receivers = ReceiverGroup.All;
        eventOptions.CachingOption = EventCaching.DoNotCache;

        PhotonNetwork.RaiseEvent(1, sendContent, eventOptions, SendOptions.SendUnreliable);
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

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == chattingEvent) 
        {
            object[] receiveObjects = (object[])photonEvent.CustomData;
            string receiveMessage = $"{receiveObjects[0].ToString()}: {receiveObjects[1].ToString()}";

            text_chat.text += receiveMessage;
        }
    }
}
