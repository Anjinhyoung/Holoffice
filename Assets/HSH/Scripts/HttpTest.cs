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
            // 가상의 나의 데이터를 만들자
            TextInfo textInfo = new TextInfo();
            textInfo.input_text = "동해물과 백두산이 마르고 닳도록 \r\n하느님이 보우하사 우리나라 만세. \r\n무궁화 삼천리 화려강산 \r\n대한 사람, 대한으로 길이 보전하세\r\n\r\n남산 위에 저 소나무, 철갑을 두른 듯 \r\n바람서리 불변함은 우리 기상일세. \r\n무궁화 삼천리 화려강산 \r\n대한 사람, 대한으로 길이 보전하세\r\n\r\n가을 하늘 공활한데 높고 구름 없이 \r\n밝은 달은 우리 가슴 일편단심일세. \r\n무궁화 삼천리 화려강산 \r\n대한 사람, 대한으로 길이 보전하세\r\n\r\n이 기상과 이 맘으로 충성을 다하여 \r\n괴로우나 즐거우나 나라 사랑하세. \r\n무궁화 삼천리 화려강산 \r\n대한 사람, 대한으로 길이 보전하세";
            

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
                // 다운로드된 데이터를 Texture2D 로 변환.
                DownloadHandlerTexture handler = downloadHandler as DownloadHandlerTexture;
                Texture2D texture = handler.texture;

                // texture 를 이용해서 Sprite 로 변환
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                Image image = GameObject.Find("Image").GetComponent<Image>();
                image.sprite = sprite;
            };
            StartCoroutine(HttpManager.GetInstance().DownloadSprite(info));
        }
    }
}
