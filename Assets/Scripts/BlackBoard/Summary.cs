
/*
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Summary : MonoBehaviour
{
    [SerializeField]
    Text note_text;

    [SerializeField]
    GameObject ai_note;

    [SerializeField]
    Text summary_object;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ai_note.SetActive(true);
            To_Summary();
        }
    }

    public void To_Summary()
    {
        TextInfo textInfo = new TextInfo();
        textInfo.input_text = note_text.text;

        HttpInfo info = new HttpInfo();
        info.url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting";
        info.body = JsonUtility.ToJson(textInfo);
        info.contentType = "application/json";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            summary_object.text = downloadHandler.text;
        };
        StartCoroutine(HttpManager.GetInstance().Post(info));
    }
}

*/

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Summary : MonoBehaviour
{
    [SerializeField]
    Text note_text;

    [SerializeField]
    GameObject ai_note;

    [SerializeField]
    Text summary_object;

    private bool isRequestInProgress = false; // 서버 요청 중인지 여부를 저장하는 플래그

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRequestInProgress) // 마우스 왼쪽 버튼 클릭 시, 요청 중이 아닐 때만 실행
        {
            ai_note.SetActive(true);
            To_Summary();
        }
    }

    public void To_Summary()
    {
        isRequestInProgress = true; // 서버 요청 시작 시 플래그를 true로 설정
        TextInfo textInfo = new TextInfo();
        textInfo.input_text = note_text.text;

        HttpInfo info = new HttpInfo();
        info.url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting";
        info.body = JsonUtility.ToJson(textInfo);
        info.contentType = "application/json";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
            summary_object.text = downloadHandler.text;
            isRequestInProgress = false; // 서버 응답이 완료되면 플래그를 false로 설정
        };
        StartCoroutine(HttpManager.GetInstance().Post(info));
    }
}