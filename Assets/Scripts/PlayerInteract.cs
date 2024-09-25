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

    PhotonView pv;

    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerMove = GetComponent<PlayerMove>();
        camController = GetComponentInChildren<CameraController>();
        model = GetComponentInChildren<Animator>().transform;   //transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");
        pv = GetComponent<PhotonView>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (!pv.IsMine) return;


        if (other.gameObject.layer == LayerMask.NameToLayer("Chair"))
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                RPC_Sit(other.transform.position, other.transform.rotation);
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
                    //camController.CamStateChange(CameraController.CamState.Computer);
                    playerMove.RPC_Write();
                }
            }
        }
    }

    public void RPC_Sit(Vector3 pos, Quaternion rot)
    {
        pv.RPC("Sit", RpcTarget.All, pos, rot);
    }

    [PunRPC]
    private void Sit(Vector3 chairPos, Quaternion chairRot)
    {
        if (playerMove.isSit == false)
        {
            //other.GetComponent<MeshCollider>().enabled = false;
            Vector3 dir = chairRot.eulerAngles;
            transform.position = chairPos + chairRot * Vector3.forward * 0.5f;

            dir = new Vector3(0, dir.y + dir.z - 180, 0);
            model.rotation = Quaternion.Euler(dir);

            StartCoroutine(SitDelay());
        }
        else if (!playerMove.isWrite)
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
        transform.position = new Vector3(transform.position.x, 0, transform.position.z)- model.transform.forward * 1.5f;
        playerMove.isSit = false;
    }
}
