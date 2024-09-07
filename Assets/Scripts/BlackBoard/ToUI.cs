using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if ���࿡ ��Ʈ���� 10���� 10���� canvas�� �ʿ��ϴ�.

    GameObject player;

    CameraController cameraController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {

        float distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        // ������ ���ؾ� �ϴµ� Dot�� �˾Ҵ�.
        // ��Ȯ�� Dot�� �� ������ ���⼺ �� �� ���� ���
        // �ٵ� ���� ������� ���� �Ŵ� �ֳ� ����� ���� �� �ؼ� ����� ������ ���� ����� �Ŵ�.


        // �÷��̾� ���� ����(��Ʈ�Ͽ��� �÷��̾� ���ϴ� ����)
        Vector3 to_Player = (this.gameObject.transform.position - player.transform.position).normalized;
        
        float distance_Angle = Vector3.Angle(this.transform.forward, to_Player); // ���� ������ �� �´� �� �� �� �����

        
        if (Input.GetKeyDown("e") && (0 <= distance && distance <= 1.85) && (135 <= distance_Angle && distance_Angle <= 138))
        {
            // print(distance); // ��Ʈ�� ���鿡�� ���� �� �տ��� ���� �� 1.7 , �ڿ����� �Ȱ���
            // print(distance_Angle); // '' 136, �̰Ŵ� 48�� ���Դ�.
            canvas.gameObject.SetActive(true);
        }
        
    }
}
