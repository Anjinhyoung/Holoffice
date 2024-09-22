
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

    private bool isRequestInProgress = false; // ���� ��û ������ ���θ� �����ϴ� �÷���

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isRequestInProgress) // ���콺 ���� ��ư Ŭ�� ��, ��û ���� �ƴ� ���� ����
        {
            ai_note.SetActive(true);
            To_Summary();
        }
    }

    public void To_Summary()
    {
        isRequestInProgress = true; // ���� ��û ���� �� �÷��׸� true�� ����
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
            isRequestInProgress = false; // ���� ������ �Ϸ�Ǹ� �÷��׸� false�� ����
        };
        StartCoroutine(HttpManager.GetInstance().Post(info));
    }
}