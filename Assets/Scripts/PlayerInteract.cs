using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private PlayerManager playerManager;
    private CameraController camController;
    private Transform model;
    private ToUI toUI;
    public PlayerMove playerMove;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerMove = GetComponent<PlayerMove>();
        camController = GetComponentInChildren<CameraController>();
        model = transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");

    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Chair"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Sit(other);
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Laptop"))
        {
            if (playerMove.isSit && !playerMove.isWrite)
            {
                toUI = other.gameObject.GetComponent<ToUI>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    toUI.OpenNote();
                    // ī�޶� ���¸� ��ǻ�� �� ���·� ��ȯ
                    camController.CamStateChange(CameraController.CamState.Computer);
                    playerMove.Write();
                }
            }
        }
    }

    private void Sit(Collider other)
    {
        if (playerMove.isSit == false)
        {
            //other.GetComponent<MeshCollider>().enabled = false;

            Vector3 dir = other.transform.localRotation.eulerAngles;
            dir = new Vector3(dir.x, -dir.z, dir.y);
            model.transform.rotation = Quaternion.Euler(dir);
            transform.position = other.transform.position + other.transform.forward * 0.5f;

            StartCoroutine(SitDelay());
        }
        else if (playerMove.isSit == true && !playerMove.isWrite)
        {
            StartCoroutine(StandDelay());
        }
    }

    

    private IEnumerator SitDelay()
    {
        playerMove.SitAni();
        playerMove.isSit = true;
        yield return new WaitForSeconds(1.5f);
    }

    private IEnumerator StandDelay()
    {
        playerMove.SitAni();
        yield return new WaitForSeconds(2.5f);
        playerMove.isSit = false;
    }
}
