using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviourPun
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
        model = GetComponentInChildren<Animator>().transform;   //transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");

    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        PhotonView pv = other.GetComponent<PhotonView>();

        if (pv.IsMine)
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
                        // 카메라 상태를 컴퓨터 줌 상태로 변환
                        camController.CamStateChange(CameraController.CamState.Computer);
                        playerMove.Write();
                    }
                }
            }
        }
    }


    [PunRPC]
    private void Sit(Collider other)
    {
        if (playerMove.isSit == false)
        {
            //other.GetComponent<MeshCollider>().enabled = false;

            Vector3 dir = other.transform.rotation.eulerAngles;

            Debug.Log(dir);

            dir = new Vector3(0, dir.y + dir.z - 180, 0);
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
