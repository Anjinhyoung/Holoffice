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
            Debug.Log("�ƹ�Ÿ ������ �迭�� ����ְų� ������ �ȵǾ���");

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