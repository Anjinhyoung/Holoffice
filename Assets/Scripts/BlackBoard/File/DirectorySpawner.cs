using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System;

public class DirectorySpawner : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textDirectoryName; // 현재 경로 이름을 나타내는 Text UI
    [SerializeField]
    Scrollbar verticalScrollbar; // 폴더, 파일들이 배치되는 ScrollView의 스크롤바

    [SerializeField]
    GameObject panelDataPrefab; // 현재 폴더에 존재하는 폴더, 파일의 파일명(Icon)을 나타내는 프리팹
    [SerializeField]
    Transform parentContent; // 생성되는 Text UI가 저장되는 부모 오브젝트 (ScrollView의 Content)

    DirectoryController directoryController; // DirectoryController 주소 정보. Data 클래스에 전달

    List<Data> fileList; // 현재 폴더에 존재하는 파일 리스트

    public void Setup(DirectoryController controller)
    {
        directoryController = controller;

        // 현재 폴더에 존재하는 디렉토리, 파일 오브젝트 리스트
        fileList = new List<Data>();
    }

    // 현재 경로에 존재하는 폴더, 파일의 Text UI 생성

    public void UpdateDirectory(DirectoryInfo currentDirectory)
    {
        // 기존에 생성되에 있는 데이터 정보 삭제
        for(int i = 0; i < fileList.Count; ++i)
        {
            Destroy(fileList[i].gameObject);
        }
        fileList.Clear();

        // 현재 폴더 이름 출력
        textDirectoryName.text = currentDirectory.Name;

        // Scrollbar value를 1로 설정해서 목록의 가장 위로 이동
        verticalScrollbar.value = 1;

        // 상위 폴더로 이동하기 위한 "..."생성
        SpawnData("...", DataType.Directory);

        // 현재 폴더에 존재하는 모든 폴더 Text UI 생성 [Directorys]
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            SpawnData(directory.Name, DataType.Directory);
        }

        // 현재 폴더에 존재하는 모든 파일 Text UI 생성[Files]
        foreach(FileInfo file in currentDirectory.GetFiles())
        {
            SpawnData(file.Name, DataType.File);
        }

        // 폴더, 파일 정보가 저장되어 있는 리스트를 FileName 오름차순으로 정렬
        fileList.Sort((a, b) => a.FileName.CompareTo(b.FileName)); // 람다식 사용

        // 정렬이 완려된 fileList를 기준으로 화면에 배치된 오브젝트도 재정렬
        // 상위 폴더로 이동하는 "..."는 제일 위에 배치
        for(int i = 0; i < fileList.Count; ++i)
        {
            fileList[i].transform.SetSiblingIndex(i);

            if (fileList[i].FileName.Equals("..."))
            {
                fileList[i].transform.SetAsFirstSibling();
            }
        }
    }

    private void SpawnData(string fileName, DataType type)
    {
        GameObject clone = Instantiate(panelDataPrefab);

        // 생성한 Panel UI를 Content 자식으로 배치하고, 크기를 1로 설정
        clone.transform.SetParent(parentContent);
        clone.transform.localScale = Vector3.one;

        Data data = clone.GetComponent<Data>();
        data.Setup(directoryController, fileName, type);

        // 파일 정렬, 삭제를 위해 리스트에 저장
        fileList.Add(data);
    }
}
