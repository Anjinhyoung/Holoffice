using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera Camera;
    public Transform target;
    public float distance = 7f;
    public float delay = 10f;
    public float sitOffsetY = 1.6f;

    private float rotSpeed = 200f;
    private float curRotX = 0f;
    private float curRotY = 0f;
    private Vector3 dir;
    private Quaternion rotation;
    private Vector3 curPos;
    private Vector3 goPos;
    private bool isSit = false;

    void Start()
    {
        Vector3 angles = Camera.transform.eulerAngles;
        curRotX = angles.y;
        curRotY = angles.x;
        curPos = transform.position;

        // CamPos�� Ÿ�� ������ �Ÿ��� ��Ÿ���� ����
        dir = new Vector3(0, 0, -distance);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            CamRot();

        if (Input.GetKeyDown(KeyCode.F))
            ToggleSit();
    }

    private void LateUpdate()
    {
        CamMove();
    }

    void CamRot()
    {
        float mx = Input.GetAxis("Mouse X") * rotSpeed * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * rotSpeed * Time.deltaTime;

        curRotX += mx;
        curRotY -= my;
        curRotY = Mathf.Clamp(curRotY, -30, 60);

        // CamPos�� �˵� ȸ��
        rotation = Quaternion.Euler(curRotY, curRotX, 0);

        if (!isSit)
        {
            Vector3 lookAt = transform.position;
            transform.LookAt(lookAt);
        }
    }

    void CamMove()
    {
        Vector3 lastPos;

        if(isSit)
        {
            lastPos = target.position + Vector3.up * sitOffsetY;

            goPos = lastPos;
            // ī�޶��� ��ġ ����
            curPos = goPos;
            Camera.transform.position = curPos;

            Camera.transform.rotation = rotation;
        }
        else
        {
        // CamPos ��ġ ����
            transform.position = target.position + rotation * dir;
            lastPos = target.position + rotation * dir;
            lastPos = WallCheck(transform.position);

            goPos = lastPos;
            // ī�޶��� ��ġ ����
            curPos = Vector3.Lerp(curPos, goPos, delay * Time.deltaTime);
            Camera.transform.position = curPos;

            // ī�޶��� ���� ����
            Vector3 lookAt = target.position;
            Camera.transform.LookAt(lookAt);
        }

       
    }

    Vector3 WallCheck(Vector3 pos)
    {
        RaycastHit hit;
        Vector3 dir = pos - target.position;
        float distance = Vector3.Distance(target.position, pos);

        if (Physics.Raycast(target.position, dir, out hit, distance, 1<<LayerMask.NameToLayer("Wall")))
        {
            return hit.point + (target.position - hit.point).normalized * 0.7f;
        }

        return pos;
    }


    void ToggleSit()
    {
        isSit = !isSit;
        if (isSit)  
        {
            curPos = target.position + Vector3.up * sitOffsetY;
            Camera.transform.position = curPos;
        }
    }
}
