using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class Test_search : MonoBehaviour
{
   InputField input_word;

    void Awake()
    {
        input_word = GetComponent<InputField>();
    }

    void Start()
    {
        // InputField에 이벤트 리스너 등록
        input_word.onSubmit.AddListener(Search_123);
    }
    

    public void Search_123(string search_word)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string encodedSearchTerm = UnityEngine.Networking.UnityWebRequest.EscapeURL(search_word);
            // UnityWebRequest는 Unity에서 웹 통신을 처리하고 강력하고 유연한 도구 => 이 부분 질문하기 
            // 그러나 내 코드는 웹 통신보다는 단순한 웹 브라우저를 여는 것에 가깝다.
            string url = "https://www.google.co.kr/search?q=" + encodedSearchTerm;
            Application.OpenURL(url);
        }
    }
}



