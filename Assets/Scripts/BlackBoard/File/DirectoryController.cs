using System.Collections;
using UnityEngine;
using System.IO; // DirectoryInfo, FileInfo class
using System;

public class DirectoryController : MonoBehaviour
{
    [SerializeField]
    FileLoadSystem fileLoaderSystem; // Ȯ���ں� ���� ó�� �ý���(Load & Play) 
    DirectoryInfo defaultDirectory; // �⺻ ����(����ȭ��) escŰ ������ ���� ������ ����ȭ������ ���ư��� �ϱ�

    DirectoryInfo currentDirectory; // ���� ����
    DirectorySpawner directorySpawner; // ���� ��ο� �ִ� ����, ���� ���� ����/ ���� ����

    private void Awake()
    {
        // ���α׷��� �ֻ�ܿ� Ȱ��ȭ ���°� �ƴϾ �÷��� => ����Ƽ�� ��Ŀ���� �Ұų� �ٸ� â�� Ȱ��ȭ�Ǵ��� ������ �ߴܵ��� �ʰ� ��� �۵�
        Application.runInBackground = true;

        directorySpawner = GetComponent<DirectorySpawner>();
        directorySpawner.Setup(this);

        // ���� ��θ� ����ȭ������ ����
        // Environment.GetFolderPath() : �����쿡 �����ϴ� ���� ��θ� ������ �޼ҵ�
        // Environment.SpecialFolder: �����쿡 �����ϴ� Ư�� ���� ������
        string desktopFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        defaultDirectory = new DirectoryInfo(desktopFolder);
        currentDirectory = new DirectoryInfo(desktopFolder);

        // ���� ������ �����ϴ� ���丮, ���� ����
        UpdateDirectory(currentDirectory);
    }

    private void Update()
    {
        // ESC Ű�� ������ �� ����ȭ�� ������ �̵�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UpdateDirectory(defaultDirectory);
        }
        // �齺���̽� Ű�� ������ �� ���� ������ �̵�
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            MoveToParentFloder(currentDirectory);
        }
    }

    // ���� ���� ���� ������Ʈ
    void UpdateDirectory(DirectoryInfo directory)
    {
        // ���� ��� ����
        currentDirectory = directory;

        // ���� ������ �����ϴ� ��� ����, ���� PanelData ���� (��� �Լ� ���)
        directorySpawner.UpdateDirectory(currentDirectory);
    }


    // ���� ������ �̵�
    private void MoveToParentFloder(DirectoryInfo directory)
    {
        // ���� ������ ������ ����
        if (directory.Parent == null) return;

        UpdateDirectory(directory.Parent);
    }


    public void UpdateInputs(string data)
    {
        // 1. ������ ���(data)�� "..."�̸� ���� ������ �̵�
        if (data.Equals("..."))
        {
            MoveToParentFloder(currentDirectory);

            return;
        }

        //2. ������ ���(data)�� �����̸� ������ ���� ���η� �̵�
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            if (data.Equals(directory.Name))
            {
                UpdateDirectory(directory);

                return;
            }
        }


        // 3, ������ ���(data)�� �����̸� Ȯ���ڿ� ���� ó�� => �̰Ŵ� �� �� ���
        foreach(FileInfo file in currentDirectory.GetFiles())
        {
            if (data.Equals(file.Name))
            {
                // Debug.Log($"������ ������ �̸�: {file.FullName}");
                fileLoaderSystem.LoadFile(file);
            }
        }
    }

}
