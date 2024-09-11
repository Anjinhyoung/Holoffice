using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if ���࿡ ��Ʈ���� 10���� 10���� canvas�� �ʿ��ϴ�.

    GameObject player;

    CameraController cameraController;

    private float distance;
    private float distance_Angle;
    private bool CanOpenNote = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {
        NoteCheck();
    }

    void NoteCheck()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        // ������ ���ؾ� �ϴµ� Dot�� �˾Ҵ�.
        // ��Ȯ�� Dot�� �� ������ ���⼺ �� �� ���� ���
        // �ٵ� ���� ������� ���� �Ŵ� �ֳ� ����� ���� �� �ؼ� ����� ������ ���� ����� �Ŵ�.


        // �÷��̾� ���� ����(��Ʈ�Ͽ��� �÷��̾� ���ϴ� ����)
        Vector3 to_Player = (this.gameObject.transform.position - player.transform.position).normalized;

        distance_Angle = Vector3.Angle(this.transform.forward, to_Player); // ���� ������ �� �´� �� �� �� �����

        if ((0 <= distance && distance <= 1.85) && (120 <= distance_Angle && distance_Angle <= 160))
        {
            CanOpenNote = true;
        }
        else
        {
            CanOpenNote = false;
        }
    }

    public void OpenNote()
    {
        if (CanOpenNote)
        {
            // print(distance); // ��Ʈ�� ���鿡�� ���� �� �տ��� ���� �� 1.7 , �ڿ����� �Ȱ���
            // print(distance_Angle); // '' 136, �̰Ŵ� 48�� ���Դ�.
            canvas.gameObject.SetActive(true);
        }
    }
}
