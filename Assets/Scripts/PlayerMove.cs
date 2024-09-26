using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMove : MonoBehaviourPun, IPunObservable
{
    private CharacterController cc;
    public Transform model;
    private PlayerManager playerManager;
    Animator animator;
    Transform cam;
    PhotonView pv;
    Vector3 myPos;
    Vector3 movedir;
    Vector3 myRot;
    public TMP_Text nickName;
    PlaySceneUI playUI;

    public float moveSpeed = 5;     // ����� �̵��ӵ� 
    public bool isSit = false;
    public bool isWrite = false;
    float temp = 1;

    private void Awake()
    {
        cam = Camera.main.transform;
        playerManager = FindObjectOfType<PlayerManager>();      // PlayerManager�� ���� ������Ʈ ã�� (�ϳ��� �����Ұ�� ���)
        cc = GetComponent<CharacterController>();
        model = GetComponentInChildren<Animator>().gameObject.transform;   //transform.Find(playerManager.avatarPrefabs[playerManager.AvatarNum()].name + "(Clone)");    // Instantiate�� �����Ͽ� �ڿ� (Clone) �߰�
        pv = GetComponent<PhotonView>();

        //Cursor.lockState = CursorLockMode.Confined;

        animator = GetComponentInChildren<Animator>();

        nickName.text = photonView.Owner.NickName;

        playUI = FindObjectOfType<PlaySceneUI>();
    }


    void Update()
    {
        if (playUI.chatOpen) return;

        if (!isSit)
        {
            Move();
        }

        if (!isWrite && pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                pv.RPC("HandUP", RpcTarget.All);
            }
        }
    }


    void Move()
    {
        if (pv.IsMine)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 dir = new Vector3(h, 0, v);

            if (dir.magnitude > 0.1f)
            {
                animator.SetBool("IsWalk", true);

                Vector3 forward = cam.forward;
                Vector3 right = cam.right;

                forward.y = 0f;
                right.y = 0f;

                forward.Normalize();
                right.Normalize();

                movedir = (forward * dir.z + right * dir.x).normalized;

                model.transform.forward = Vector3.Lerp(model.transform.forward, movedir, Time.deltaTime * 10);

                cc.Move(movedir * moveSpeed * Time.deltaTime);

            }
            else
            {
                animator.SetBool("IsWalk", false);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, myPos) > 0.1f)
            {
                animator.SetBool("IsWalk", true);
                model.transform.forward = Vector3.Lerp(model.transform.forward, myRot, Time.deltaTime * 10);
            }
            else
            {
                animator.SetBool("IsWalk",false);
            }
            transform.position = Vector3.Lerp(transform.position, myPos, Time.deltaTime * 10);
        }
        
    }

    public void RPC_SitAni()
    {
        pv.RPC("SitAni", RpcTarget.All);
    }

    [PunRPC]
    public void SitAni()
    {
        animator.SetBool("IsSit", !isSit);
    }

    public void RPC_Write()
    {
        pv.RPC("Write", RpcTarget.All);
    }

    [PunRPC]
    public void Write()
    {
        animator.SetBool("IsWrite", !isWrite);

        isWrite = !isWrite;
    }

    [PunRPC]
    public void HandUP()
    {
        animator.SetLayerWeight(1, 1);
        animator.SetTrigger("HandRaise");
        StartCoroutine(HandSet());
    }

    public IEnumerator HandSet()
    {
        yield return new WaitForSeconds(3f);

        animator.SetLayerWeight(1, 0);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // iterable �����͸� ������
            stream.SendNext(transform.position);
            stream.SendNext(movedir);
        }
        else if (stream.IsReading)
        {
            myPos = (Vector3)stream.ReceiveNext();
            myRot = (Vector3)stream.ReceiveNext();
        }
    }
}
