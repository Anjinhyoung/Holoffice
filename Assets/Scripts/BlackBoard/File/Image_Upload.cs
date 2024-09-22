using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;


public class Image_Upload : MonoBehaviour
{
    [SerializeField]
    GameObject panelImageViewer; // �̹��� ������ ����ϴ� Panel

    [SerializeField]
    Image imageDrawTexture; // ������ ��Ÿ���� �̹��� ���

    [SerializeField]
    TextMeshProUGUI textFileData; // ���� �̸�, �ػ�, �뷮

    float maxWidth = 850; // Image UI �ִ� ũ��
    float maxHeight = 555;

    public void OnLoad(FileInfo file)
    {
        // �̹��� ������ ����ϴ� Panel Ȱ��ȭ
        panelImageViewer.SetActive(true);

        // ���Ϸκ��� Bytes �����͸� �ҷ��´�.
        byte[] byteTexture = File.ReadAllBytes(file.FullName);

        // byteTexture�� �ִ� byte �迭 ������ �������� Texture2D �̹��� ���� ������ ����
        Texture2D texture2D = new Texture2D(0, 0);

        if(byteTexture.Length > 0)
        {
            texture2D.LoadImage(byteTexture);
        }

        // �̹����� ����ϴ� Image UI�� ũ�� ����
        // ���� Texture�� width ũ�Ⱑ Image UI�� �ִ� width ũ�⺸�� ũ��
        if(texture2D.width > maxWidth)
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxWidth, maxWidth / texture2D.width * texture2D.height);
        }

        // ���� �ؽ�ó�� height ũ�Ⱑ Image UI�� �ִ� height ũ�⺸�� ũ��

        else if(texture2D.height > maxHeight)
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(maxHeight / texture2D.height * texture2D.width, maxHeight);
        }
        else
        {
            imageDrawTexture.rectTransform.sizeDelta = new Vector2(texture2D.width, texture2D.height);
        }

        // Texture2D -> Sprite ��ȯ
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));

        // imageDrawTexture Image UI�� �������� �̹����� sprite�� ����
        imageDrawTexture.sprite = sprite;

        // �̹��� ���� ���� ���
        textFileData.text = $"{file.Name} ({texture2D.width} x {texture2D.height}, {file.Length}Bytes)"; 
    }

    public void OffLoad()
    {
        // �̹��� ������ ����ϴ� Panel ��Ȱ��ȭ
        panelImageViewer.SetActive(false);
    }
}
