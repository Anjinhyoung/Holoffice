using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;



[System.Serializable]
public struct TextInfo
{
    public string input_text;
}
public class HttpTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Get
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                print(downloadHandler.text);
                //string jsonData = "{\"data\" : "+ downloadHandler.text + "}";
                //print(jsonData);
                //allPostInfo = JsonUtility.FromJson<PostInfoArray>(jsonData);
            };
            StartCoroutine(HttpManager.GetInstance().Get(info));
        }

        // Post
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // ������ ���� �����͸� ������
            TextInfo textInfo = new TextInfo();
            textInfo.input_text = "���ع��� ��λ��� ������ �⵵�� \r\n�ϴ����� �����ϻ� �츮���� ����. \r\n����ȭ ��õ�� ȭ������ \r\n���� ���, �������� ���� �����ϼ�\r\n\r\n���� ���� �� �ҳ���, ö���� �θ� �� \r\n�ٶ����� �Һ����� �츮 ����ϼ�. \r\n����ȭ ��õ�� ȭ������ \r\n���� ���, �������� ���� �����ϼ�\r\n\r\n���� �ϴ� ��Ȱ�ѵ� ���� ���� ���� \r\n���� ���� �츮 ���� ����ܽ��ϼ�. \r\n����ȭ ��õ�� ȭ������ \r\n���� ���, �������� ���� �����ϼ�\r\n\r\n�� ���� �� ������ �漺�� ���Ͽ� \r\n���ο쳪 ��ſ쳪 ���� ����ϼ�. \r\n����ȭ ��õ�� ȭ������ \r\n���� ���, �������� ���� �����ϼ�";
            

            HttpInfo info = new HttpInfo();
            info.url = "https://snake-hopeful-urchin.ngrok-free.app/summarize_meeting";
            info.body = JsonUtility.ToJson(textInfo);
            info.contentType = "application/json";
            //info.contentType = "text";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                print(downloadHandler.text);
            };
            StartCoroutine(HttpManager.GetInstance().Post(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://e098-222-103-183-137.ngrok-free.app/redoc";
            info.contentType = "pngFile";
            info.body = "C:\\Users\\Admin\\Desktop\\Karina.png";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                File.WriteAllBytes(Application.dataPath + "/aespa.jpg", downloadHandler.data);
            };
            StartCoroutine(HttpManager.GetInstance().UploadFilebyByte(info));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            HttpInfo info = new HttpInfo();
            info.url = "https://search.pstatic.net/common/?src=http%3A%2F%2Fimgnews.naver.net%2Fimage%2F5291%2F2024%2F05%2F30%2F0002032341_001_20240530002810731.jpg&type=a340";
            info.onComplete = (DownloadHandler downloadHandler) =>
            {
                // �ٿ�ε�� �����͸� Texture2D �� ��ȯ.
                DownloadHandlerTexture handler = downloadHandler as DownloadHandlerTexture;
                Texture2D texture = handler.texture;

                // texture �� �̿��ؼ� Sprite �� ��ȯ
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                Image image = GameObject.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            };
            StartCoroutine(HttpManager.GetInstance().DownloadSprite(info));
        }
    }
}
