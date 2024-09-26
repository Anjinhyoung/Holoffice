using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Realtime;
using static Unity.VisualScripting.Member;

public class WebCamScript : MonoBehaviourPun, IPunObservable, IOnEventCallback
{
    public RawImage webcamDisplay;
    public Image voiceIcon;
    public GameObject panel_faceChat;
    float sendSecond;
    //public int portNumber = 5000;
    //public string myIP = "192.168.0.38";

    WebCamTexture webcamTexture;
    PhotonView pv;
    PhotonVoiceView voiceView;
    bool isTalking = false;

    //Thread udpThread;
    //UdpClient receivePort;
    //UdpClient sendPort;

    Texture2D sendTex;
    Texture2D recievedTex;

    const byte webcamEvent = 2;

    Coroutine webCamCoroutine;
    

    public Button btn_WebCam;

    //void InitializeUDPThread()
    //{
    //    // ��׶��忡�� �� Thread�� �����ϰ� �ʹ�.
    //    udpThread = new Thread(new ThreadStart(ReceiveData));
    //    udpThread.IsBackground = true;
    //    udpThread.Start();
    //}

    //void ReceiveData()
    //{
    //    receivePort = new UdpClient(portNumber);
    //    IPEndPoint remoteClient = new IPEndPoint(IPAddress.Any, portNumber);
    //    try
    //    {
    //        while(true)
    //        {
    //            byte[] bins = receivePort.Receive(ref remoteClient);
    //            recievedTex = Base64ToTexture2D(bins);
    //        }

    //    }
    //    catch (SocketException message)
    //    {
    //        // ��� ���� �ڵ� �� ���� ������ ����Ѵ�.
    //        Debug.LogError($"Error Code {message.ErrorCode} - {message}");
    //    }
    //    finally
    //    {
    //        receivePort.Close();
    //    }
    //}

    //void SendData()
    //{
    //    // Ŭ���̾�Ʈ�μ� �غ�
    //    sendPort = new UdpClient(portNumber);
    //    sendTex = new Texture2D(webcamTexture.width, webcamTexture.height);
    //    sendTex.SetPixels32(webcamTexture.GetPixels32());
    //    byte[] binData = sendTex.EncodeToPNG();

    //    // �����͸� �����Ѵ�.
    //    sendPort.Send(binData, binData.Length, myIP, 7000);
    //}

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.AddCallbackTarget(this);
    }
  
    void Start()
    {
        //InitializeUDPThread();

        //webcamDisplay = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
        pv = GetComponent<PhotonView>();
        voiceView = GetComponent<PhotonVoiceView>();
        // ��� ��ķ ��ġ ����� �����ɴϴ�.
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No webcam detected.");
            return;
        }

        // ù ��° ��ķ�� ����Ͽ� WebCamTexture�� ����
        webcamTexture = new WebCamTexture(devices[0].name);
        webcamTexture.requestedWidth = 50;
        webcamTexture.requestedHeight = 20;

        // RawImage ������Ʈ�� ��ķ ������ �Ҵ�


        webcamDisplay.texture = webcamTexture;
        webcamDisplay.enabled = false;
        panel_faceChat.SetActive(false);
        sendSecond = 0.2f;

        btn_WebCam = GameObject.Find("Btn_Cam").GetComponent<Button>();

        if(pv.IsMine)
        {
            btn_WebCam.onClick.AddListener(RPC_PlayWebCam);
        }

    }

    void Update()
    {
        if(voiceView != null)
        {
            if (pv.IsMine)
            {
                // ���� ���� �ϰ� �ִٸ� ���̽� �������� Ȱ��ȭ�Ѵ�.
                voiceIcon.gameObject.SetActive(voiceView.IsRecording);

            }
            else
            {
                voiceIcon.gameObject.SetActive(isTalking);
            }
        }


        //if(webcamDisplay.enabled)
        //{
        //    SendWebCam();
        //}

    }


    public void OnEvent(EventData photonEvent)
    {
        if(photonEvent.Code == webcamEvent)
        {
            object[] recievedObjects = (object[])photonEvent.CustomData;
            recievedTex = Base64ToTexture2D((byte[])recievedObjects[0]);
            if(!pv.IsMine)
            {
                webcamDisplay.texture = recievedTex;
            }
        }
    }

    IEnumerator SendWebCam()
    {
        yield return new WaitUntil(() => webcamTexture.isPlaying);

        while (webcamDisplay.enabled)
        {
            //Color32[] test = new Color32[webcamTexture.width * webcamTexture.height];
            sendTex = new Texture2D((webcamTexture.width), (webcamTexture.height));

            sendTex.SetPixels32(webcamTexture.GetPixels32());
            sendTex.Apply();
            sendTex = ScaleTexture(sendTex, 0.3f);

            byte[] binData = sendTex.EncodeToPNG();

            // �ɼ�
            RaiseEventOptions eventOptions = new RaiseEventOptions();
            eventOptions.Receivers = ReceiverGroup.All;
            eventOptions.CachingOption = EventCaching.DoNotCache;

            object[] sendContent = new[] { binData };

            // �̺�Ʈ �۽�   
            PhotonNetwork.RaiseEvent(2, sendContent, eventOptions, SendOptions.SendUnreliable);

            yield return new WaitForSeconds(sendSecond);
        }
        yield return null;
    }

    
    private Texture2D Base64ToTexture2D(byte[] imageData)
    {
        int width, height;
        GetImageSize(imageData, out width, out height);

        Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false, true);

        texture.hideFlags = HideFlags.HideAndDontSave;
        texture.filterMode = FilterMode.Point;
        texture.LoadImage(imageData);
        texture.Apply();

        return texture;
    }
    private void GetImageSize(byte[] imageData, out int width, out int height)
    {
        width = ReadInt(imageData, 3 + 15);
        height = ReadInt(imageData, 3 + 15 + 2 + 2);
    }

    private int ReadInt(byte[] imageData, int offset)
    {
        return (imageData[offset] << 8 | imageData[offset + 1]);
    }

    void RPC_PlayWebCam()
    {
        pv.RPC("PlayWebCam", RpcTarget.AllBuffered);

        if (pv.IsMine)
        {
            if (webcamDisplay.enabled)
            {
                webcamTexture.Play();
                webCamCoroutine = StartCoroutine(SendWebCam());
            }
            else
            {
                if (webCamCoroutine != null)
                {
                    StopCoroutine(webCamCoroutine);
                }
                webcamTexture.Stop();
            }
        }
    } 

    [PunRPC]
    void PlayWebCam()
    {
        webcamDisplay.enabled = !webcamDisplay.enabled;
        panel_faceChat.SetActive(webcamDisplay.enabled);
        //print(webcamDisplay.enabled ? "true" : "false");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ����, �����͸� ������ ����(PhotonView.IsMine == true)�ϴ� ���¶��...
        if (stream.IsWriting)
        {
            stream.SendNext(voiceView.IsRecording);
        }
        // �׷��� �ʰ�, ���� �����͸� �����κ��� �о���� ���¶��...
        else if (stream.IsReading)
        {
            isTalking = (bool)stream.ReceiveNext();
        }
    }

    public Texture2D ScaleTexture(Texture2D source, float _scaleFactor)
    {
        if (_scaleFactor == 1f)
        {
            return source;
        }
        else if (_scaleFactor == 0f)
        {
            return Texture2D.blackTexture;
        }

        int _newWidth = Mathf.RoundToInt(source.width * _scaleFactor);
        int _newHeight = Mathf.RoundToInt(source.height * _scaleFactor);



        Color[] _scaledTexPixels = new Color[_newWidth * _newHeight];

        for (int _yCord = 0; _yCord < _newHeight; _yCord++)
        {
            float _vCord = _yCord / (_newHeight * 1f);
            int _scanLineIndex = _yCord * _newWidth;

            for (int _xCord = 0; _xCord < _newWidth; _xCord++)
            {
                float _uCord = _xCord / (_newWidth * 1f);

                _scaledTexPixels[_scanLineIndex + _xCord] = source.GetPixelBilinear(_uCord, _vCord);
            }
        }

        // Create Scaled Texture
        Texture2D result = new Texture2D(_newWidth, _newHeight, source.format, false);
        result.SetPixels(_scaledTexPixels, 0);
        result.Apply();

        return result;
    }

    private void OnDisable()
    {
        //// UDP ��Ʈ���� �����Ѵ�.
        //receivePort.Close();

        PhotonNetwork.NetworkingClient.RemoveCallbackTarget(this);
    }
}
