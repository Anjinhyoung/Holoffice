using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using System;

public class MP3Loader : MonoBehaviour
{
    [Header("MP3 Player Panel, File Name")]
    [SerializeField]
    GameObject panelMP3Player; // 음원 재생 제어를 위한 Panel
    [SerializeField]
    TextMeshProUGUI textFileName; // 파일 이름 Text

    [Header("MP3 Play Time (Slider, Text)")]
    [SerializeField]
    Slider sliderPlaybar; // 재생 시간을 나타내는 Slider
    [SerializeField]
    TextMeshProUGUI textCurrentPlaytime; // 현재 재생시간 Text
    [SerializeField]
    TextMeshProUGUI textMaxPlaytime; // 총 재생시간 Text

    [Header("Play Audio")]
    [SerializeField]
    AudioSource audioSource; // 음원 재생용 AudioSource

    public void OnLoad(System.IO.FileInfo file)
    {
        // Panel 활성화
        panelMP3Player.SetActive(true);

        // MP3 파일 이름 출력
        textFileName.text = file.Name;

        // 재생시간 표시에 사용되는 Slider, Text 초기화
        ResetPlaytimeUI();

        // MP3 파일을 불러와서 재생
        StartCoroutine(LoadAudio(file.FullName));
    }

    private IEnumerator LoadAudio(string fileName)
    {
        AudioClip clip = null;

        fileName = "file://" +fileName;

        // fileName 파일을 MP3 AudioClop 형태로 받아와서 audioData에 저장
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(fileName, AudioType.MPEG);

        // request에 데이터를 정상적으로 모두 로드할 때 까지 대기
        yield return request.SendWebRequest();

        // 데이터 로드에 성공했을 때
        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Load Suceess :{fileName}");
            clip = DownloadHandlerAudioClip.GetContent(request);

            // MP3 재생 파일 설정
            audioSource.clip = clip;
            // MP3 재생
            Play();
        }

        // 데이터 로드에 실패했을 때
        else
        {
            Debug.Log("Load Failed");
        }
    }

    public void OffLoad()
    {
        Stop();

        // Panel 비활성화
        panelMP3Player.SetActive(false);
    }

    public void Play()
    {
        // 사운드 재생
        audioSource.Play();

        // Slider, Text에 재생시간 정보 업데이트
        StartCoroutine("OnPlaytimeUI");
    }

    public void Pause()
    {
        // 사운드 재생 일시 정지
        audioSource.Pause();
    }

    public void Stop()
    {
        // 사운드 재생 중지
        audioSource.Stop();

        // 사운드 재생 중지
        audioSource.Stop();

        // Slider, Text에 재생시간 정보 업데이트 중지
        StopCoroutine("OnPlaytimeUI");

        // 재생 시간 표시에 사용되는 Slider, Text 초기회
        ResetPlaytimeUI();
    }

    private void ResetPlaytimeUI()
    {
        sliderPlaybar.value = 0;
        textCurrentPlaytime.text = "00:00:00";
        textMaxPlaytime.text = "00:00:00";
    }

    IEnumerator OnPlaytimeUI()
    {
        int hour = 0;
        int minutes = 0;
        int seconds = 0;

        while (true)
        {
            // 현재 재생시간 표시 (Text UI)
            hour = (int)audioSource.time / 3600;
            minutes = (int)(audioSource.time%3600)/60;
            seconds = (int)(audioSource.time%3600)%60;
            textCurrentPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // 총 재생시간 표시 (Text UI)
            hour = (int)audioSource.clip.length / 3600;
            minutes = (int)(audioSource.clip.length%3600)/60;
            seconds = (int)(audioSource.clip.length % 3600) % 60;
            textMaxPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // Slider에 표시되는 재생 시간 설정
            sliderPlaybar.value = audioSource.time / audioSource.clip.length;

            yield return new WaitForSeconds(1);
        }
    }
}
