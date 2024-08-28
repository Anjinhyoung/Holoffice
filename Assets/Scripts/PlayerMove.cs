using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController cc;
    private float rotSpeed = 200f;

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

        dir = dir.normalized;
        
        if(h != 0 || v != 0)
            model.transform.forward = dir;

        cc.Move(dir * moveSpeed * Time.deltaTime);
    }

}
