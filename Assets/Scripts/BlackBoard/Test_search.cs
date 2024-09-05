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
        // InputField�� �̺�Ʈ ������ ���
        input_word.onSubmit.AddListener(Search_123);
    }
    

    public void Search_123(string search_word)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            string encodedSearchTerm = UnityEngine.Networking.UnityWebRequest.EscapeURL(search_word);
            // UnityWebRequest�� Unity���� �� ����� ó���ϰ� �����ϰ� ������ ���� => �� �κ� �����ϱ� 
            // �׷��� �� �ڵ�� �� ��ź��ٴ� �ܼ��� �� �������� ���� �Ϳ� ������.
            string url = "https://www.google.co.kr/search?q=" + encodedSearchTerm;
            Application.OpenURL(url);
        }
    }
}



