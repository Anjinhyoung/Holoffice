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
    public TMP_Text text_chat;
    public ScrollRect scroll_Chat;
    public TMP_InputField input_Chat;
    public TMP_Text playerCount;
    public GameObject RedDot;

    bool chatOpen = false;

    const byte chattingEvent = 1;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
    }

    void Start()
    {
        input_Chat.text = "";
        text_chat.text = "";

        btn_Chat.onClick.AddListener(OpenChat);
        btn_Exit.onClick.AddListener(Exit);

        input_Chat.onSubmit.AddListener(SendMyMessage);

    }

    void SendMyMessage(string msg)
    {
        object[] sendContent = new object[] { PhotonNetwork.NickName, msg };
        RaiseEventOptions eventOptions = new RaiseEventOptions();
        eventOptions.Receivers = ReceiverGroup.All;
        eventOptions.CachingOption = EventCaching.DoNotCache;

        PhotonNetwork.RaiseEvent(chattingEvent, sendContent, eventOptions, SendOptions.SendUnreliable);
    }


    void OpenChat()
    {
        chatOpen = !chatOpen;
        panel_Chat.SetActive(chatOpen);

        RedDot.SetActive(false);
    }

    void Exit()
    {

    }

    void IOnEventCallback.OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == chattingEvent) 
        {
            object[] receiveObjects = (object[])photonEvent.CustomData;
            string receiveMessage = $"{receiveObjects[0].ToString()}: {receiveObjects[1].ToString()}\n";

            text_chat.text += receiveMessage;

            input_Chat.text = "";

            GetChat();
        }
    }

    public void GetChat()
    {
        if (!chatOpen)
            RedDot.SetActive(true);
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
    }

    public void SetPlayerCount(RoomInfo room)
    {
        playerCount.text = $"({room.PlayerCount}/{room.MaxPlayers})\t{room.Name}";
    }
}
