using Photon.Pun;
using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamScript : MonoBehaviour, IPunObservable
{
    RawImage webcamDisplay;
    WebCamTexture webcamTexture;
    RawImage voiceIcon;
    PhotonView pv;
    PhotonVoiceView voiceView;
    bool isTalking = false;

    void Start()
    {
        webcamDisplay = transform.GetChild(0).GetChild(0).GetComponent<RawImage>();
        pv = GetComponent<PhotonView>();
        voiceView = GetComponent<PhotonVoiceView>();

        // 모든 웹캠 장치 목록을 가져옵니다.
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No webcam detected.");
            return;
        }

        // 첫 번째 웹캠을 사용하여 WebCamTexture를 생성
        webcamTexture = new WebCamTexture(devices[0].name);

        // RawImage 컴포넌트에 웹캠 영상을 할당
        webcamDisplay.texture = webcamTexture;
    }

    void Update()
    {
        if (pv.IsMine)
        {
            // 현재 말을 하고 있다면 보이스 아이콘을 활성화한다.
            voiceIcon.gameObject.SetActive(voiceView.IsRecording);
            if (Input.GetKeyDown(KeyCode.M))
            {
                RPC_PlayWebCam();
            }
        }
        else
        {
            voiceIcon.gameObject.SetActive(isTalking);
        }
    }

    void RPC_PlayWebCam()
    {
        pv.RPC("PlayWebCam", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void PlayWebCam()
    {
        webcamDisplay.enabled = !webcamDisplay.enabled;
        if (webcamDisplay.enabled)
        {
            webcamTexture.Pause();
        }
        else
        {
            webcamTexture.Play();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 만일, 데이터를 서버에 전송(PhotonView.IsMine == true)하는 상태라면...
        if (stream.IsWriting)
        {
            stream.SendNext(voiceView.IsRecording);
        }
        // 그렇지 않고, 만일 데이터를 서버로부터 읽어오는 상태라면...
        else if (stream.IsReading)
        {
            isTalking = (bool)stream.ReceiveNext();
        }
    }
}
