using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEngine.Video;

public class MP4Loader : MonoBehaviour
{
    [Header("MP4 Player Panel, File Name")]
    [SerializeField]
    GameObject panelMP4Player; // 음원 재생 제어를 위한 Panel
    [SerializeField]
    TextMeshProUGUI textFileName; // 파일 이름 

    [Header("MP4 Play Time (Slider, Text)")]
    [SerializeField]
    Slider sliderPlaybar; // 재생 시간을 나타내는 Slider
    [SerializeField]
    TextMeshProUGUI textCurrentPlaytime; // 현재 재생시간 
    [SerializeField]
    TextMeshProUGUI textMaxPlaytime; // 총 재생시간 

    [Header("Play Video & Audio")]
    [SerializeField]
    RawImage rawImageDrawVideo; // 영상 출력을 위한 RawImage
    [SerializeField]
    VideoPlayer VideoPlayer; // 영상 재생용 VideoPlayer
    [SerializeField]
    AudioSource audioSource; // 음원 재생용 AudioSource

    public void OnLoad(System.IO.FileInfo file)
    {
        // Panel 활성화
        panelMP4Player.SetActive(true);

        // MP3 파일 이름 출력
        textFileName.text = file.Name;

        // 재생시간 표시에 사용되는 Slider, Text 초기화
        ResetPlaytimeUI();

        // MP3 파일을 불러와서 재생
        StartCoroutine(LoadVidio(file.FullName));
    }

    private IEnumerator LoadVidio(string fullPath)
    {
        
        VideoPlayer.url = "file://" +fullPath;

        // 동영상 소리 재생 모드: AudioSource
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        // 동영상 소리를 재생할 AudioSource 설정
        // 오디오트랙 디코딩 활성/비활성화 (VideoPlayer가 재생중이 아닐 때 설정)
        VideoPlayer.EnableAudioTrack(0, true);
        VideoPlayer.SetTargetAudioSource(0, audioSource);

        // videoPlayer에 등록된 영상의 사운드 재생으로 사용하기 때문에 audioSource.clip 비워둔다
        audioSource.clip = null;

        // 동영상에 출력되는 이미지를 imageDrawTexture에 설정(설정이 안되어 있으면 코드로 설정)
        rawImageDrawVideo.texture = VideoPlayer.targetTexture;

        // clip 정보를 동적으로 변경할 때는 Prepare() 호출 후 Prepaer가 완료되어야 재생 가능
        VideoPlayer.Prepare();

        while (!VideoPlayer.isPrepared)
        {
            yield return null;
        }

        // MP4 동영상/ 사운드 재생
        Play();
 
    }

    public void OffLoad()
    {
        Stop();

        // Panel 비활성화
        panelMP4Player.SetActive(false);
    }

    public void Play()
    {
        // 동영상, 사운드 재생
        VideoPlayer.Play();
        audioSource.Play();

        // Slider, Text에 재생시간 정보 업데이트
        StartCoroutine("OnPlaytimeUI");
    }

    public void Pause()
    {
        // 동영상, 사운드 재생 일시 정지
        VideoPlayer.Pause();
        audioSource.Pause();
    }

    public void Stop()
    {
        // 사운드 재생 중지
        VideoPlayer.Stop();
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
            hour = (int)VideoPlayer.time / 3600;
            minutes = (int)(VideoPlayer.time%3600)/60;
            seconds = (int)(VideoPlayer.time%3600)%60;
            textCurrentPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // 총 재생시간 표시 (Text UI)
            hour = (int)VideoPlayer.length / 3600;
            minutes = (int)(VideoPlayer.length%3600)/60;
            seconds = (int)(VideoPlayer.length % 3600) % 60;
            textMaxPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // Slider에 표시되는 재생 시간 설정
            sliderPlaybar.value = (float)(VideoPlayer.time / VideoPlayer.clip.length);

            yield return new WaitForSeconds(1);
        }
    }
}
