using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetOutUI : MonoBehaviour
{
    private CameraController camController;

    public Canvas canvas;

    private void Start()
    {
        camController = FindAnyObjectByType<CameraController>();
    }
    public void GetOut()
    {
        canvas.gameObject.SetActive(false);
        camController.CamStateChange(CameraController.CamState.Third);
    }
}
