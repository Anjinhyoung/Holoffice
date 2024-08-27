using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerManager
{
    private CharacterController cc;

    public float moveSpeed;

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

        dir = dir.normalized;

        cc.Move(dir * moveSpeed * Time.deltaTime);
        transform.forward = dir;
    }
}
