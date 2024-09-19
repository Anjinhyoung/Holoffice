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
