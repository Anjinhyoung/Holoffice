using UnityEngine;
using System.IO;
using System;

public class FileLoadSystem : MonoBehaviour
{
    // Ȯ���ں� ���� ó�� (Load & Play)
    FileLoader fileLoader; // ����, �Ϲ� ����
    Image_Upload imageLoader; // �̹��� ����

    private void Awake()
    {
        fileLoader = GetComponent<FileLoader>();
        imageLoader = GetComponent<Image_Upload>();
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
    }
}

