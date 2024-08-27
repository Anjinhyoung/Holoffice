using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform camPos;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camPos != null)
        {
            transform.position = camPos.position;
        }
    }
}
