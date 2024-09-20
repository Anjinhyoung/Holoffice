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
    GameObject panelMP4Player; // ���� ��� ��� ���� Panel
    [SerializeField]
    TextMeshProUGUI textFileName; // ���� �̸� 

    [Header("MP4 Play Time (Slider, Text)")]
    [SerializeField]
    Slider sliderPlaybar; // ��� �ð��� ��Ÿ���� Slider
    [SerializeField]
    TextMeshProUGUI textCurrentPlaytime; // ���� ����ð� 
    [SerializeField]
    TextMeshProUGUI textMaxPlaytime; // �� ����ð� 

    [Header("Play Video & Audio")]
    [SerializeField]
    RawImage rawImageDrawVideo; // ���� ����� ���� RawImage
    [SerializeField]
    VideoPlayer VideoPlayer; // ���� ����� VideoPlayer
    [SerializeField]
    AudioSource audioSource; // ���� ����� AudioSource

    public void OnLoad(System.IO.FileInfo file)
    {
        // Panel Ȱ��ȭ
        panelMP4Player.SetActive(true);

        // MP3 ���� �̸� ���
        textFileName.text = file.Name;

        // ����ð� ǥ�ÿ� ���Ǵ� Slider, Text �ʱ�ȭ
        ResetPlaytimeUI();

        // MP3 ������ �ҷ��ͼ� ���
        StartCoroutine(LoadVidio(file.FullName));
    }

    private IEnumerator LoadVidio(string fullPath)
    {
        
        VideoPlayer.url = "file://" +fullPath;

        // ������ �Ҹ� ��� ���: AudioSource
        VideoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        // ������ �Ҹ��� ����� AudioSource ����
        // �����Ʈ�� ���ڵ� Ȱ��/��Ȱ��ȭ (VideoPlayer�� ������� �ƴ� �� ����)
        VideoPlayer.EnableAudioTrack(0, true);
        VideoPlayer.SetTargetAudioSource(0, audioSource);

        // videoPlayer�� ��ϵ� ������ ���� ������� ����ϱ� ������ audioSource.clip ����д�
        audioSource.clip = null;

        // ������ ��µǴ� �̹����� imageDrawTexture�� ����(������ �ȵǾ� ������ �ڵ�� ����)
        rawImageDrawVideo.texture = VideoPlayer.targetTexture;

        // clip ������ �������� ������ ���� Prepare() ȣ�� �� Prepaer�� �Ϸ�Ǿ�� ��� ����
        VideoPlayer.Prepare();

        while (!VideoPlayer.isPrepared)
        {
            yield return null;
        }

        // MP4 ������/ ���� ���
        Play();
 
    }

    public void OffLoad()
    {
        Stop();

        // Panel ��Ȱ��ȭ
        panelMP4Player.SetActive(false);
    }

    public void Play()
    {
        // ������, ���� ���
        VideoPlayer.Play();
        audioSource.Play();

        // Slider, Text�� ����ð� ���� ������Ʈ
        StartCoroutine("OnPlaytimeUI");
    }

    public void Pause()
    {
        // ������, ���� ��� �Ͻ� ����
        VideoPlayer.Pause();
        audioSource.Pause();
    }

    public void Stop()
    {
        // ���� ��� ����
        VideoPlayer.Stop();
        audioSource.Stop();

        // Slider, Text�� ����ð� ���� ������Ʈ ����
        StopCoroutine("OnPlaytimeUI");

        // ��� �ð� ǥ�ÿ� ���Ǵ� Slider, Text �ʱ�ȸ
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
            // ���� ����ð� ǥ�� (Text UI)
            hour = (int)VideoPlayer.time / 3600;
            minutes = (int)(VideoPlayer.time%3600)/60;
            seconds = (int)(VideoPlayer.time%3600)%60;
            textCurrentPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // �� ����ð� ǥ�� (Text UI)
            hour = (int)VideoPlayer.length / 3600;
            minutes = (int)(VideoPlayer.length%3600)/60;
            seconds = (int)(VideoPlayer.length % 3600) % 60;
            textMaxPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // Slider�� ǥ�õǴ� ��� �ð� ����
            sliderPlaybar.value = (float)(VideoPlayer.time / VideoPlayer.clip.length);

            yield return new WaitForSeconds(1);
        }
    }
}
