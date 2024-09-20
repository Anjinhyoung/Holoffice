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

        // ��� ��ķ ��ġ ����� �����ɴϴ�.
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No webcam detected.");
            return;
        }

        // ù ��° ��ķ�� ����Ͽ� WebCamTexture�� ����
        webcamTexture = new WebCamTexture(devices[0].name);

        // RawImage ������Ʈ�� ��ķ ������ �Ҵ�
        webcamDisplay.texture = webcamTexture;
    }

    void Update()
    {
        if (pv.IsMine)
        {
            // ���� ���� �ϰ� �ִٸ� ���̽� �������� Ȱ��ȭ�Ѵ�.
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
}
