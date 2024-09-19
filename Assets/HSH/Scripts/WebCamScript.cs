using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WebCamScript : MonoBehaviour
{
    public RawImage webcamDisplay;
    WebCamTexture webcamTexture;

    void Start()
    {
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
        if(Input.GetKeyDown(KeyCode.Alpha5) && webcamTexture.isPlaying == false)
        {
            webcamTexture.Play();
            webcamDisplay.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha6) && webcamTexture.isPlaying == true)
        {
            webcamTexture.Pause();
            webcamDisplay.enabled = false;
        }
    }
}
