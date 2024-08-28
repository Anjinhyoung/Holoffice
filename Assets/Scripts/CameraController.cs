using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public Transform target;
    public float distance = 7f;
    public float offsetX = 1f;
    public float offsetY = -1f;

    private float rotSpeed = 200f;
    private float curRotX = 0f;
    private float curRotY = 0f;
    private Vector3 dir;
    private Quaternion rotation;

    void Start()
    {
        Vector3 angles = Camera.transform.eulerAngles;
        curRotX = angles.y;
        curRotY = angles.x;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            CamRot();
    }

    private void LateUpdate()
    {
        // ī�޶��� ��ġ ����
        Camera.transform.position = transform.position;

        // ī�޶��� ���� ����
        Vector3 lookAt = target.position + new Vector3(offsetX, offsetY, 0);
        Camera.transform.LookAt(lookAt);
    }

    void CamRot()
    {
        float mx = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;

        curRotX += mx;
        curRotY -= my;

        curRotY = Mathf.Clamp(curRotY, -30, 60);

        // CamPos�� Ÿ�� ������ �Ÿ��� ��Ÿ���� ����
        dir = new Vector3(-offsetX, -offsetY, -distance);

        // CamPos�� �˵� ȸ��
        rotation = Quaternion.Euler(curRotY, curRotX, 0);
        transform.position = target.position + rotation * dir;

        Vector3 lookAt = transform.position + new Vector3(-offsetX, -offsetY, 0);
        transform.LookAt(lookAt);
    }

}
