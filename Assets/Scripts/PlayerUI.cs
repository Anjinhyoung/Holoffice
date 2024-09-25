using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerUI : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.forward = transform.position - camera.transform.position;
    }
}
