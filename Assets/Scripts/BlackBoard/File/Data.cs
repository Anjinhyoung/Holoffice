using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

// ������ �Ӽ�(����, ����) - ������ ��¿� (������ Ȯ���ڿ� ���� ���� ������ ��� Ÿ�� �߰�)
public enum DataType { Directory = 0, File}

public class Data : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField]
    Sprite[] spriteIcons; // �����ܿ� ������ �� �ִ� Sprite �̹���
    Image imageIcon; // ������ �Ӽ��� ���� ������ ���
    TextMeshProUGUI textDataName; // ������ �̸� ���

    DataType dataType; // ������ �Ӽ�

    string fileName; // ���� �̸�
    public string FileName => fileName; //  �ܺο��� Ȯ���ϱ� ���� Get ������Ƽ(�б� ����) => �� �� ���������� �����ϱ� ���� �ʱ�ȭ vs ������Ƽ

    int maxFileNameLength = 25; // ���� �̸��� �ִ� ����

    DirectoryController directoryController;

    public void Setup(DirectoryController controller, string fileName, DataType dataType)
    {
        directoryController = controller;

        // ���� PanelData ������Ʈ�� Image ������Ʈ�� �������� �ʾ����� PanelData�� Get�ȴ�.
        imageIcon = GetComponentInChildren<Image>();
        textDataName = GetComponentInChildren<TextMeshProUGUI>();

        this.fileName = fileName;
        this.dataType = dataType;

        // ������ �̹��� ����
        imageIcon.sprite = spriteIcons[(int)this.dataType];

        // ���� �̸� ���
        textDataName.text = this.fileName;
        // ���� �̸��� �ִ� ���� maxFileNameLegth�� �Ѿ�� �̸��� �޺κ��� �߶󳻰� ".." �߰�
        if(fileName.Length >= maxFileNameLength)
        {
            textDataName.text = fileName.Substring(0, maxFileNameLength);
            textDataName.text += "..";
        }

        // ���� �̸� ���� ���� (����= �����, ����= �Ͼ��)
        SetTextColor();
    }

    
    // ���콺�� ���� �ؽ�Ʈ UI�� ���� ���� �� ȣ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        textDataName.color = Color.red;
    }

    // ���콺�� ���� Text UI�� Ŭ������ �� ȣ�� 
    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ����(����)�� Ŭ������ �� ó��
        directoryController.UpdateInputs(fileName);
    }

    // ���콺�� ���� Text UI ������ ����� �� ȣ��
    public void OnPointerExit(PointerEventData eventData)
    {
        SetTextColor();
    }


    private void SetTextColor()
    {
        if (dataType == DataType.Directory)
        {
            textDataName.color = Color.yellow;
        }
        else
        {
            textDataName.color = Color.white;
        }
    }


}
