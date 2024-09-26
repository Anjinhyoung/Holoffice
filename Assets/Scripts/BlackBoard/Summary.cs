
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

    private bool isRequestInProgress = false; // ���� ��û ������ ���θ� �����ϴ� �÷���

    public void To_Summary()
    {
        if (isRequestInProgress) return; // ��û ���̸� �ߺ� ��û ����
        isRequestInProgress = true; // ���� ��û ���� �� �÷��׸� true�� ����

        string inputText = note_text.text;

        // �α� �߰�: inputText�� ������ ����Ͽ� ���� �ؽ�Ʈ�� ��Ȯ���� Ȯ��
        Debug.Log("Original inputText: " + inputText);

        string url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting?input_text=" + UnityWebRequest.EscapeURL(inputText);

        // �α� �߰�: ���������� ������� URL�� Ȯ��
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

        isRequestInProgress = false; // ���� ������ �Ϸ�Ǹ� �÷��׸� false�� ����
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
    public string summary; // JSON���� "summary"��� �ʵ带 �޴´ٰ� ����
}


public class Summary : MonoBehaviour
{
    [SerializeField]
    TMP_Text note_text;

    [SerializeField]
    GameObject ai_note;

    [SerializeField]
    Text summary_object;

    private bool isRequestInProgress = false; // ���� ��û ������ ���θ� �����ϴ� �÷���

    [System.Serializable]
    public class SummaryResponse
    {
        public string summary; // JSON ���信�� summary �ʵ�
    }

    public void To_Summary()
    {
        if (isRequestInProgress) return; // ��û ���̸� �ߺ� ��û ����
        isRequestInProgress = true; // ���� ��û ���� �� �÷��׸� true�� ����

        string inputText = note_text.text;

        // �α� �߰�: inputText�� ������ ����Ͽ� ���� �ؽ�Ʈ�� ��Ȯ���� Ȯ��
        Debug.Log("Original inputText: " + inputText);

        string url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting?input_text=" + UnityWebRequest.EscapeURL(inputText);

        // �α� �߰�: ���������� ������� URL�� Ȯ��
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
            // JSON �Ľ�
            SummaryResponse response = JsonUtility.FromJson<SummaryResponse>(webRequest.downloadHandler.text);

            // ���� ���� summary ���
            summary_object.text = "Summary: " + response.summary;
        }

        isRequestInProgress = false; // ���� ������ �Ϸ�Ǹ� �÷��׸� false�� ����
    }
}

