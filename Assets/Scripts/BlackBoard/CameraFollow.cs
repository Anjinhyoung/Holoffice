using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject Player;

    Transform playerTf;

    
    private void Awake()
    {
        playerTf = Player.GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        this.transform.position = new Vector3(playerTf.position.x, playerTf.position.y + 1.0f, playerTf.position.z);
    }



}
