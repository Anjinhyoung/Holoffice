using System.Collections;
using UnityEngine;
using System.IO; // DirectoryInfo, FileInfo class
using System;

public class DirectoryController : MonoBehaviour
{
    [SerializeField]
    FileLoadSystem fileLoaderSystem; // 확장자별 파일 처리 시스템(Load & Play) 
    DirectoryInfo defaultDirectory; // 기본 폴더(바탕화면) esc키 누르면 현재 폴더가 바탕화면으로 돌아가게 하기

    DirectoryInfo currentDirectory; // 현재 폴더
    DirectorySpawner directorySpawner; // 현재 경로에 있는 폴더, 파일 정보 생성/ 삭제 제어

    private void Awake()
    {
        // 프로그램이 최상단에 활성화 상태가 아니어도 플레이 => 유니티가 포커스를 잃거나 다른 창이 활성화되더라도 실행이 중단되지 않고 계속 작동
        Application.runInBackground = true;

        directorySpawner = GetComponent<DirectorySpawner>();
        directorySpawner.Setup(this);

        // 최초 경로를 바탕화면으로 설정
        // Environment.GetFolderPath() : 윈도우에 존재하는 폴더 경로를 얻어오는 메소드
        // Environment.SpecialFolder: 윈도우에 존재하는 특수 폴더 열거형
        string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        defaultDirectory = new DirectoryInfo(desktopFolder);
        currentDirectory = new DirectoryInfo(desktopFolder);

        // 현재 폴더에 존재하는 디렉토리, 파일 생성
        UpdateDirectory(currentDirectory);
    }

    private void Update()
    {
        // ESC 키를 눌렀을 때 바탕화면 폴더로 이동
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateDirectory(defaultDirectory);
        }
        // 백스페이스 키를 눌렀을 때 상위 폴더로 이동
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MoveToParentFloder(currentDirectory);
        }
    }

    // 현재 폴더 정보 업데이트
    void UpdateDirectory(DirectoryInfo directory)
    {
        // 현재 경로 설정
        currentDirectory = directory;

        // 현재 폴더에 존재하는 모든 폴더, 파일 PanelData 생성 (재귀 함수 사용)
        directorySpawner.UpdateDirectory(currentDirectory);
    }


    // 상위 폴더로 이동
    private void MoveToParentFloder(DirectoryInfo directory)
    {
        // 상위 폴더가 없으면 종료
        if (directory.Parent == null) return;

        UpdateDirectory(directory.Parent);
    }


    public void UpdateInputs(string data)
    {
        // 1. 선택한 목록(data)이 "..."이면 상위 폴더로 이동
        if (data.Equals("..."))
        {
            MoveToParentFloder(currentDirectory);

            return;
        }

        //2. 선택한 목록(data)이 폴더이면 선택한 폴더 내부로 이동
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            if (data.Equals(directory.Name))
            {
                UpdateDirectory(directory);

                return;
            }
        }


        // 3, 선택한 목록(data)이 파일이면 확장자에 따라 처리 => 이거는 좀 더 고민
        foreach(FileInfo file in currentDirectory.GetFiles())
        {
            if (data.Equals(file.Name))
            {
                // Debug.Log($"선택한 파일의 이름: {file.FullName}");
                fileLoaderSystem.LoadFile(file);
            }
        }
    }

}
