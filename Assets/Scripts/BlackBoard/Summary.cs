
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


/*
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class Summary : MonoBehaviour
{
    [SerializeField]
    TMP_Text note_text;

    [SerializeField]
    GameObject ai_note;

    [SerializeField]
    Text summary_object;

    private bool isRequestInProgress = false; // 서버 요청 중인지 여부를 저장하는 플래그

    public void To_Summary()
    {
        if (isRequestInProgress) return; // 요청 중이면 중복 요청 방지
        isRequestInProgress = true; // 서버 요청 시작 시 플래그를 true로 설정

        string inputText = note_text.text;

        // 로그 추가: inputText의 내용을 출력하여 원본 텍스트가 정확한지 확인
        Debug.Log("Original inputText: " + inputText);

        string url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting?input_text=" + UnityWebRequest.EscapeURL(inputText);

        // 로그 추가: 최종적으로 만들어진 URL을 확인
        Debug.Log("Generated URL: " + url);

        StartCoroutine(GetRequest(url));
        ai_note.SetActive(true);
    }

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            summary_object.text = webRequest.downloadHandler.text;
        }

        isRequestInProgress = false; // 서버 응답이 완료되면 플래그를 false로 설정
    }
}
*/

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class SummaryResponse
{
    public string summary; // JSON에서 "summary"라는 필드를 받는다고 가정
}


public class Summary : MonoBehaviour
{
    [SerializeField]
    TMP_Text note_text;

    [SerializeField]
    GameObject ai_note;

    [SerializeField]
    Text summary_object;

    private bool isRequestInProgress = false; // 서버 요청 중인지 여부를 저장하는 플래그

    [System.Serializable]
    public class SummaryResponse
    {
        public string summary; // JSON 응답에서 summary 필드
    }

    public void To_Summary()
    {
        if (isRequestInProgress) return; // 요청 중이면 중복 요청 방지
        isRequestInProgress = true; // 서버 요청 시작 시 플래그를 true로 설정

        string inputText = note_text.text;

        // 로그 추가: inputText의 내용을 출력하여 원본 텍스트가 정확한지 확인
        Debug.Log("Original inputText: " + inputText);

        string url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting?input_text=" + UnityWebRequest.EscapeURL(inputText);

        // 로그 추가: 최종적으로 만들어진 URL을 확인
        Debug.Log("Generated URL: " + url);

        StartCoroutine(GetRequest(url));
        ai_note.SetActive(true);
    }

    IEnumerator GetRequest(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + webRequest.error);
        }
        else
        {
            // JSON 파싱
            SummaryResponse response = JsonUtility.FromJson<SummaryResponse>(webRequest.downloadHandler.text);

            // 보기 좋게 summary 출력
            summary_object.text = "Summary: " + response.summary;
        }

        isRequestInProgress = false; // 서버 응답이 완료되면 플래그를 false로 설정
    }
}

