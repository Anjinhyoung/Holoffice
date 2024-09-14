using UnityEngine;
using TMPro;
using System.IO;

public class FileLoader : MonoBehaviour
{
    [SerializeField]
    GameObject panelFileViewer; // ���� ������ ����ϴ� Panel

    [SerializeField]
    TextMeshProUGUI textFileName; // ���� �̸�

    [SerializeField]
    TextMeshProUGUI textFileSize; // ���� ũ��

    [SerializeField]
    TextMeshProUGUI textCreationTime; // ���� �����ð�

    [SerializeField]
    TextMeshProUGUI textLastWriteTime; // ���� ���� ���� �ð�

    [SerializeField]
    TextMeshProUGUI textDirectory; // ���� ���

    [SerializeField]
    TextMeshProUGUI textFullName; // ��ü ���(���丮 + ���� �̸�)

    FileInfo fileInfo; // ���� ������ ��Ÿ���� FileInfo

    public void OnLoad(FileInfo file)
    {
        // ���� ������ ����ϴ� Panel Ȱ��ȭ
        panelFileViewer.SetActive(true);

        fileInfo = file;

        // ������ ������ Text UI�� ���
        textFileName.text = $"���� �̸�: {fileInfo.Name}";
        textFileSize.text = $"���� ũ��: {fileInfo.Length} Bytes";
        textCreationTime.text = $"���� ���� �ð�: {fileInfo.CreationTime}";
        textLastWriteTime.text = $"���� ���� ���� �ð�: {fileInfo.LastWriteTime}";
        textDirectory.text = $"���� ���: {fileInfo.Directory}";
        textFullName.text = $"��ü ���: {fileInfo.FullName}";
    }

    public void OpenFile()
    {
        // ���� ����
        Application.OpenURL("file:///" + fileInfo.FullName);
    }

    public void offLoad()
    {
        // ���� ������ ����ϴ� Panel ��Ȱ��ȭ
        panelFileViewer.SetActive(false);
    }
}
