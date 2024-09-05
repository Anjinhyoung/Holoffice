using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class Summary : MonoBehaviour
{
    string note;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            To_Summary();
        }
    }

    public void To_Summary()
    {
        note = GameObject.Find("note").GetComponent<Text>().text;
        HttpInfo info = new HttpInfo();
        info.url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting";
        info.body = JsonUtility.ToJson(note);
        info.contentType = "application/json";
        info.onComplete = (DownloadHandler downloadHandler) =>
        {
            print(downloadHandler.text);
        };
        StartCoroutine(HttpManager.GetInstance().Post(info));
    }
}
