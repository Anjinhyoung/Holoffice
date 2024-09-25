using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class PlayerManager : MonoBehaviourPun
{
    public string playerName = null;
    public bool isSeat = false;
    public GameObject[] avatarPrefabs;

    private int avatarIndex = 0;
    private GameObject currentAvatarInstance;
    private GameObject player;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        PhotonNetwork.SerializationRate = 30;
        PhotonNetwork.SendRate = 30;
    }

    public void SetSelectedAvatar(int index)
    {
        if (index >= 0 && index < avatarPrefabs.Length)
        {
            avatarIndex = index;
            PlayerPrefs.SetInt("SelectedAvatarIndex", index);
            PlayerPrefs.Save();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "PlayScene")
        {
            LoadSelectedAvatar();
        }
    }

    void LoadSelectedAvatar()
    {
        //avatarIndex = PlayerPrefs.GetInt("SelectedAvatarIndex", 0);

        SpawnSelectedAvatar();
    }

    void SpawnSelectedAvatar()
    {
        if(avatarIndex >= 0 && avatarIndex < avatarPrefabs.Length)
        {
            StartCoroutine(SpawnPlayer());
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public int AvatarNum()
    {
        return avatarIndex;
    }

    IEnumerator SpawnPlayer()
    {
        yield return new WaitUntil(() => { return PhotonNetwork.InRoom; });

        player = PhotonNetwork.Instantiate("Player" + avatarIndex, Vector3.zero, Quaternion.identity);
        if (player != null)
        {
            currentAvatarInstance = player;

            Animator animtor = currentAvatarInstance.GetComponentInChildren<Animator>();

            animtor.applyRootMotion = false;
        }
        else
        {
            Debug.LogError("플레이어 오브젝트가 없다!");
        }
    }


}
