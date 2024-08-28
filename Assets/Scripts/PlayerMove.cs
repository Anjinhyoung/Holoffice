using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;

    public Transform cam;
    public GameObject model;
    public float moveSpeed = 7;     // 사용자 이동속도 

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (dir.magnitude > 0.1f)
        {
            Vector3 forward = cam.forward;
            Vector3 right = cam.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 movedir = (forward * dir.z + right * dir.x).normalized;

            model.transform.forward = movedir;

            cc.Move(movedir * moveSpeed * Time.deltaTime);
        }
    }
}
