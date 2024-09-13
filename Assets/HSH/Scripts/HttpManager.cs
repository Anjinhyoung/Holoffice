using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;


public class HttpInfo
{
    public string url = "";

    // body ������
    public string body = "";

    // content type
    public string contentType = "";

    //  ��� ������ ȣ��Ǵ� �Լ� �Ǵ� ����
    public Action<DownloadHandler> onComplete;

}

public struct TextInfo
{
    public string input_text;
}
public class HttpManager : MonoBehaviour
{
    static HttpManager instance;

    public static HttpManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.name = "httpManager";
            go.AddComponent<HttpManager>();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // https://e098-222-103-183-137.ngrok-free.app/redoc

    
    
    public IEnumerator Get(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(info.url))
        {
            // ������ ��û ������
            yield return webRequest.SendWebRequest();
            // �������� ������ �Դ�
            DoneRequest(webRequest, info);
        }
    }
    
    

    public IEnumerator Post(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(info.url, info.body, info.contentType))
        {
            // ������ ��û ������
            yield return webRequest.SendWebRequest();
            // �������� ������ �Դ�
            DoneRequest(webRequest, info);
        }
    }

    
    public IEnumerator UploadFilebyByte(HttpInfo info)
    {
        // Infodata ���� ������ ��ġ
        // info.data���ִ� ������ byte �迭�� �о����
        byte[] data = File.ReadAllBytes(info.body);

        using (UnityWebRequest webRequest = new UnityWebRequest(info.url, "POST"))
        {

            // ���ε� �ϴ� ������
            webRequest.uploadHandler = new UploadHandlerRaw(data);
            webRequest.uploadHandler.contentType = info.contentType;

            //����޴� ������ ����
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // ������ ��û ������
            yield return webRequest.SendWebRequest();
            // �������� ������ �Դ�
            DoneRequest(webRequest, info);

        }
    }

    public IEnumerator DownloadSprite(HttpInfo info)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(info.url))
        {
            yield return webRequest.SendWebRequest();

            DoneRequest(webRequest, info);
        }
    }
    
    

    void DoneRequest(UnityWebRequest webRequest, HttpInfo info)
    {
        // ������ ����� �����̶��
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            // ����� �����͸� ��û�� Ŭ������ ������.
            if (info.onComplete != null)
            {
                info.onComplete(webRequest.downloadHandler);
            }
        }
        // �׷����ʴٸ�(error ���)
        else
        {
            // error �� ������ ���
            Debug.LogError("������ : " + webRequest.error);
        }
    }
}
