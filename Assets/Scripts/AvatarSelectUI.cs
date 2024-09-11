using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AvatarSelectionUI : MonoBehaviour
{
    public Button nextButton;
    public Button prevButton;
    public Button selectButton;
    public Button lobbyButton;
    public PlayerManager playerManager;
    public Transform previewPoint;

    private int currAvatarIndex = 0;
    private GameObject currPreviewAvatar;

    void Start()
    {
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        if (playerManager.avatarPrefabs == null || playerManager.avatarPrefabs.Length == 0)
        {
            Debug.Log("아바타 프리펩 배열이 비어있거나 세팅이 안되었다");

            return;
        }

        nextButton.onClick.AddListener(NextAvatar);
        prevButton.onClick.AddListener(PrevAvatar);
        selectButton.onClick.AddListener(SelectAvatar);
        lobbyButton.onClick.AddListener(GoLobby);


        ShowAvatarPreview(currAvatarIndex);
    }

    void NextAvatar()
    {
        currAvatarIndex = (currAvatarIndex + 1) % playerManager.avatarPrefabs.Length;
        ShowAvatarPreview(currAvatarIndex);
    }

    void PrevAvatar()
    {
        currAvatarIndex = (currAvatarIndex - 1 + playerManager.avatarPrefabs.Length) % playerManager.avatarPrefabs.Length;
        ShowAvatarPreview(currAvatarIndex);
    }

    void SelectAvatar()
    {
        playerManager.SetSelectedAvatar(currAvatarIndex);

        SceneManager.LoadScene("PlayScene");
    }

    void GoLobby()
    {
        SceneManager.LoadScene("");
    }

    void ShowAvatarPreview(int index)
    {
        if (currPreviewAvatar != null)
        {
            Destroy(currPreviewAvatar);
        }

        currPreviewAvatar = Instantiate(playerManager.avatarPrefabs[index], previewPoint.position, previewPoint.rotation);
    }
}