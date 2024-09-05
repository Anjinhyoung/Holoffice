using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jinhyoung_Dumy_Player : MonoBehaviour
{
    CharacterController cc;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = transform.TransformDirection(new Vector3(h, 0, v));
        direction.Normalize();

        cc.Move(direction * 2.0f * Time.deltaTime);
    }
}
