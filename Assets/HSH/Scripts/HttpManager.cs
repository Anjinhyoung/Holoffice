using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class HttpInfo
{
    public string url = "";

    // body 데이터
    public string body = "";

    // content type
    public string contentType = "";

    //  통신 성공후 호출되는 함수 되는 변수
    public Action<DownloadHandler> onComplete;

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
            // 서버에 요청 보내기
            yield return webRequest.SendWebRequest();
            // 서버에게 응답이 왔다
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
        // 응답의 결과가 정상이라면
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            // 응답온 데이터를 요청한 클래스로 보내자.
            if (info.onComplete != null)
            {
                info.onComplete(webRequest.downloadHandler);
            }
        }
        // 그렇지않다면(error 라면)
        else
        {
            // error 의 이유를 출력
            Debug.LogError("에러는 : " + webRequest.error);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
