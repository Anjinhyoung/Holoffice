using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public string playerName = null;
    public bool isSeat = false;
    public GameObject[] avatarPrefabs;

    private int avatarIndex = 0;
    private GameObject currentAvatarInstance;


    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (scene.name == "HG")
        {
            LoadSelectedAvatar();
        }
    }

    void LoadSelectedAvatar()
    {
        avatarIndex = PlayerPrefs.GetInt("SelectedAvatarIndex", 0);
        SpawnSelectedAvatar();
    }

    void SpawnSelectedAvatar()
    {
        if (currentAvatarInstance != null)
        {
            Destroy(currentAvatarInstance);
        }

        if(avatarIndex >= 0 && avatarIndex < avatarPrefabs.Length)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                currentAvatarInstance = Instantiate(avatarPrefabs[avatarIndex], playerObject.transform);
                currentAvatarInstance.transform.localPosition = Vector3.zero;
                currentAvatarInstance.transform.localRotation = Quaternion.identity;

                Animator animtor = currentAvatarInstance.GetComponent<Animator>();
                if (animtor == null)
                {
                    animtor = currentAvatarInstance.AddComponent<Animator>();
                }

                animtor.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("PlayerAni");

                animtor.applyRootMotion = false;
            }
            else
            {
                Debug.LogError("플레이어 오브젝트가 없다!");
            }
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
}
