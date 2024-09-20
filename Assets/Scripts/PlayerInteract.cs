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
        if (pv != null)
        {
            pv = GetComponent<PhotonView>();
        }


        if (other.gameObject.layer == LayerMask.NameToLayer("Chair"))
        {
            if (pv.IsMine)
            {
                if (Input.GetKeyDown(KeyCode.C))
                {
                    RPC_Sit(other.transform.position, other.transform.forward, other.transform.rotation);
                }
            }
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Laptop"))
        {
            if (playerMove.isSit && !playerMove.isWrite && pv.IsMine)
            {
                toUI = other.gameObject.GetComponent<ToUI>();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    toUI.OpenNote();
                    // ī�޶� ���¸� ��ǻ�� �� ���·� ��ȯ
                    camController.CamStateChange(CameraController.CamState.Computer);
                    playerMove.RPC_Write();
                }
            }
        }
    }

    public void RPC_Sit(Vector3 pos, Vector3 front, Quaternion rot)
    {
        pv.RPC("Sit", RpcTarget.All, pos, front, rot);
    }

    [PunRPC]
    private void Sit(Vector3 pos, Vector3 front, Quaternion rot)
    {
        if (playerMove.isSit == false)
        {
            //other.GetComponent<MeshCollider>().enabled = false;
            Vector3 dir = rot.eulerAngles;

            Debug.Log(dir);

            dir = new Vector3(0, dir.y + dir.z - 180, 0);
            model.transform.rotation = Quaternion.Euler(dir);
            transform.position = pos + front * 0.5f;

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
        transform.position = new Vector3(transform.position.x, 0, transform.position.z)- model.transform.forward * 1.5f;
        playerMove.isSit = false;
    }
}
