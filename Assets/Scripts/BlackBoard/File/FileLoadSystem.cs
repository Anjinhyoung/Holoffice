using UnityEngine;
using System.IO;
using System;

public class FileLoadSystem : MonoBehaviour
{
    // Ȯ���ں� ���� ó�� (Load & Play)
    FileLoader fileLoader; // ����, �Ϲ� ����
    Image_Upload imageLoader; // �̹��� ����
    MP3Loader mp3Loader; // MP3 ����
    MP4Loader mp4Loader;
    private void Awake()
    {
        fileLoader = GetComponent<FileLoader>();
        imageLoader = GetComponent<Image_Upload>();
        mp3Loader = GetComponent<MP3Loader>();
        mp4Loader = GetComponent<MP4Loader>();
    }

    public void LoadFile(FileInfo file)
    {
        OffAllPanel();

        // ������ ������ ���� ������ ��� ���� ���α׷� ����
        if (file.FullName.Contains(".pdf") || file.FullName.Contains(".xlsx") || file.FullName.Contains(".doc") || file.FullName.Contains(".pptx")
            || file.FullName.Contains(".hwp") || file.FullName.Contains(".txt"))
        {
            fileLoader.OnLoad(file);
        }


        // ������ ������ �̹��� ������ ���  ȭ�鿡 �̹��� ���
        else if (file.FullName.Contains(".jpg") || file.FullName.Contains(".png"))
        {
            imageLoader.OnLoad(file);
        }

        // ������ ������ ���� ������ ��� ���� ���
        else if (file.FullName.Contains(".mp3"))
        {
            mp3Loader.OnLoad(file);
        }

        // ������ ������ ������ ������ ��� ȭ�鿡 ������ ���
        else if (file.FullName.Contains(".mp4"))
        {
            mp4Loader.OnLoad(file);
        }

        // ������ ��� Ȯ���ڴ� ������ �����ϰ� ���� ���� ���
        else
        {
            fileLoader.OnLoad(file);
        }
    }

    private void OffAllPanel()
    {
        fileLoader.offLoad();
        imageLoader.OffLoad();
        mp3Loader.OffLoad();
        mp4Loader.OffLoad();
    }
}

