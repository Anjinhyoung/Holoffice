using UnityEngine;
using System;  // �ý��� ���� ����� ����ϱ� ���� �߰�

public class Save : MonoBehaviour
{
    public void Button_On()
    {
        // ���� ������� ����ȭ�� ��θ� ����
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // ����ȭ�� ��ο� ���� �̸� �߰�
        string filePath = System.IO.Path.Combine(desktopPath, "ȸ�Ƿ� ����.png");

        // ��ũ������ ����ȭ�鿡 ����
        ScreenCapture.CaptureScreenshot(filePath);
    }
}
