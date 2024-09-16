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
    GameObject panelMP3Player; // ���� ��� ��� ���� Panel
    [SerializeField]
    TextMeshProUGUI textFileName; // ���� �̸� Text

    [Header("MP3 Play Time (Slider, Text)")]
    [SerializeField]
    Slider sliderPlaybar; // ��� �ð��� ��Ÿ���� Slider
    [SerializeField]
    TextMeshProUGUI textCurrentPlaytime; // ���� ����ð� Text
    [SerializeField]
    TextMeshProUGUI textMaxPlaytime; // �� ����ð� Text

    [Header("Play Audio")]
    [SerializeField]
    AudioSource audioSource; // ���� ����� AudioSource

    public void OnLoad(System.IO.FileInfo file)
    {
        // Panel Ȱ��ȭ
        panelMP3Player.SetActive(true);

        // MP3 ���� �̸� ���
        textFileName.text = file.Name;

        // ����ð� ǥ�ÿ� ���Ǵ� Slider, Text �ʱ�ȭ
        ResetPlaytimeUI();

        // MP3 ������ �ҷ��ͼ� ���
        StartCoroutine(LoadAudio(file.FullName));
    }

    private IEnumerator LoadAudio(string fileName)
    {
        AudioClip clip = null;

        fileName = "file://" +fileName;

        // fileName ������ MP3 AudioClop ���·� �޾ƿͼ� audioData�� ����
        UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(fileName, AudioType.MPEG);

        // request�� �����͸� ���������� ��� �ε��� �� ���� ���
        yield return request.SendWebRequest();

        // ������ �ε忡 �������� ��
        if(request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Load Suceess :{fileName}");
            clip = DownloadHandlerAudioClip.GetContent(request);

            // MP3 ��� ���� ����
            audioSource.clip = clip;
            // MP3 ���
            Play();
        }

        // ������ �ε忡 �������� ��
        else
        {
            Debug.Log("Load Failed");
        }
    }

    public void OffLoad()
    {
        Stop();

        // Panel ��Ȱ��ȭ
        panelMP3Player.SetActive(false);
    }

    public void Play()
    {
        // ���� ���
        audioSource.Play();

        // Slider, Text�� ����ð� ���� ������Ʈ
        StartCoroutine("OnPlaytimeUI");
    }

    public void Pause()
    {
        // ���� ��� �Ͻ� ����
        audioSource.Pause();
    }

    public void Stop()
    {
        // ���� ��� ����
        audioSource.Stop();

        // ���� ��� ����
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
            hour = (int)audioSource.time / 3600;
            minutes = (int)(audioSource.time%3600)/60;
            seconds = (int)(audioSource.time%3600)%60;
            textCurrentPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // �� ����ð� ǥ�� (Text UI)
            hour = (int)audioSource.clip.length / 3600;
            minutes = (int)(audioSource.clip.length%3600)/60;
            seconds = (int)(audioSource.clip.length % 3600) % 60;
            textMaxPlaytime.text = $"{hour:D2}:{minutes:D2}:{seconds:D2}";

            // Slider�� ǥ�õǴ� ��� �ð� ����
            sliderPlaybar.value = audioSource.time / audioSource.clip.length;

            yield return new WaitForSeconds(1);
        }
    }
}
