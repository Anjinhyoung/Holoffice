using UnityEngine;
using System.IO;
using System;

public class FileLoadSystem : MonoBehaviour
{
    // 확장자별 파일 처리 (Load & Play)
    FileLoader fileLoader; // 문서, 일반 파일
    Image_Upload imageLoader; // 이미지 파일

    private void Awake()
    {
        fileLoader = GetComponent<FileLoader>();
        imageLoader = GetComponent<Image_Upload>();
    }

    public void LoadFile(FileInfo file)
    {
        OffAllPanel();

        // 선택한 파일이 문서 파일일 경우 문서 프로그램 실행
        if (file.FullName.Contains(".pdf") || file.FullName.Contains(".xlsx") || file.FullName.Contains(".doc") || file.FullName.Contains(".pptx")
            || file.FullName.Contains(".hwp") || file.FullName.Contains(".txt"))
        {
            fileLoader.OnLoad(file);
        }


        // 선택한 파일이 이미지 파일일 경우  화면에 이미지 출력
        else if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png"))
        {
            imageLoader.OnLoad(file);
        }

        // 나머지 모든 확장자는 문서와 동일하게 파일 정보 출력
        else
        {
            fileLoader.OnLoad(file);
        }
    }

    private void OffAllPanel()
    {
        fileLoader.offLoad();
        imageLoader.OffLoad();
    }
}

