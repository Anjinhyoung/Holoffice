using UnityEngine;
using System;  // 시스템 관련 기능을 사용하기 위해 추가

public class Save : MonoBehaviour
{
    public void Button_On()
    {
        // 현재 사용자의 바탕화면 경로를 얻음
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        // 바탕화면 경로에 파일 이름 추가
        string filePath = System.IO.Path.Combine(desktopPath, "회의록 저장.png");

        // 스크린샷을 바탕화면에 저장
        ScreenCapture.CaptureScreenshot(filePath);
    }
}
