using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutUI : MonoBehaviour
{
    private CameraController camController;
    private PlayerMove playerMove;

    public Canvas canvas;

    private void Start()
    {
        camController = FindAnyObjectByType<CameraController>();
        playerMove = FindAnyObjectByType<PlayerMove>();
    }
    public void GetOut()
    {
        canvas.gameObject.SetActive(false);
        camController.CamStateChange(CameraController.CamState.Third);
        playerMove.Write();
    }
}
