using UnityEngine;
using UnityEngine.UI;

public class ToUI : MonoBehaviour
{
    
    public Canvas canvas; // if 만약에 노트북이 10대라면 10개의 canvas가 필요하다.

    GameObject player;

    CameraController cameraController;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    void Update()
    {

        float distance = Vector3.Distance(this.gameObject.transform.position, player.transform.position);

        // 각도를 구해야 하는데 Dot을 알았다.
        // 정확히 Dot은 두 벡터의 방향성 비교 및 각도 계산
        // 근데 아직 사용하지 않을 거다 왜냐 제대로 공부 안 해서 제대로 공부한 다음 사용할 거다.


        // 플레이어 방향 벡터(노트북에서 플레이어 향하는 벡터)
        Vector3 to_Player = (this.gameObject.transform.position - player.transform.position).normalized;
        
        float distance_Angle = Vector3.Angle(this.transform.forward, to_Player); // 내가 생각한 게 맞는 지 한 번 물어보기

        
        if (Input.GetKeyDown("e") && (0 <= distance && distance <= 1.85) && (135 <= distance_Angle && distance_Angle <= 138))
        {
            // print(distance); // 노트북 정면에서 거의 맨 앞에서 했을 때 1.7 , 뒤에서도 똑같고
            // print(distance_Angle); // '' 136, 이거는 48이 나왔다.
            canvas.gameObject.SetActive(true);
        }
        
    }
}
