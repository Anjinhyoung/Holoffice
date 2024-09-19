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
    TextMeshProUGUI textDirectoryName; // ���� ��� �̸��� ��Ÿ���� Text UI
    [SerializeField]
    Scrollbar verticalScrollbar; // ����, ���ϵ��� ��ġ�Ǵ� ScrollView�� ��ũ�ѹ�

    [SerializeField]
    GameObject panelDataPrefab; // ���� ������ �����ϴ� ����, ������ ���ϸ�(Icon)�� ��Ÿ���� ������
    [SerializeField]
    Transform parentContent; // �����Ǵ� Text UI�� ����Ǵ� �θ� ������Ʈ (ScrollView�� Content)

    DirectoryController directoryController; // DirectoryController �ּ� ����. Data Ŭ������ ����

    List<Data> fileList; // ���� ������ �����ϴ� ���� ����Ʈ

    public void Setup(DirectoryController controller)
    {
        directoryController = controller;

        // ���� ������ �����ϴ� ���丮, ���� ������Ʈ ����Ʈ
        fileList = new List<Data>();
    }

    // ���� ��ο� �����ϴ� ����, ������ Text UI ����

    public void UpdateDirectory(DirectoryInfo currentDirectory)
    {
        // ������ �����ǿ� �ִ� ������ ���� ����
        for(int i = 0; i < fileList.Count; ++i)
        {
            Destroy(fileList[i].gameObject);
        }
        fileList.Clear();

        // ���� ���� �̸� ���
        textDirectoryName.text = currentDirectory.Name;

        // Scrollbar value�� 1�� �����ؼ� ����� ���� ���� �̵�
        verticalScrollbar.value = 1;

        // ���� ������ �̵��ϱ� ���� "..."����
        SpawnData("...", DataType.Directory);

        // ���� ������ �����ϴ� ��� ���� Text UI ���� [Directorys]
        foreach(DirectoryInfo directory in currentDirectory.GetDirectories())
        {
            SpawnData(directory.Name, DataType.Directory);
        }

        // ���� ������ �����ϴ� ��� ���� Text UI ����[Files]
        foreach(FileInfo file in currentDirectory.GetFiles())
        {
            SpawnData(file.Name, DataType.File);
        }

        // ����, ���� ������ ����Ǿ� �ִ� ����Ʈ�� FileName ������������ ����
        fileList.Sort((a, b) => a.FileName.CompareTo(b.FileName)); // ���ٽ� ���

        // ������ �Ϸ��� fileList�� �������� ȭ�鿡 ��ġ�� ������Ʈ�� ������
        // ���� ������ �̵��ϴ� "..."�� ���� ���� ��ġ
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

        // ������ Panel UI�� Content �ڽ����� ��ġ�ϰ�, ũ�⸦ 1�� ����
        clone.transform.SetParent(parentContent);
        clone.transform.localScale = Vector3.one;

        Data data = clone.GetComponent<Data>();
        data.Setup(directoryController, fileName, type);

        // ���� ����, ������ ���� ����Ʈ�� ����
        fileList.Add(data);
    }
}
