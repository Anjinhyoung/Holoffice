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
    public GameObject[] avatarPrefabs;
    public Transform previewPoint;

    private int currAvatarIndex = 0;
    private GameObject currPreviewAvatar;

    void Start()
    {
        if (avatarPrefabs == null || avatarPrefabs.Length == 0)
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
        currAvatarIndex = (currAvatarIndex + 1) % avatarPrefabs.Length;
        ShowAvatarPreview(currAvatarIndex);
    }

    void PrevAvatar()
    {
        currAvatarIndex = (currAvatarIndex - 1 + avatarPrefabs.Length) % avatarPrefabs.Length;
        ShowAvatarPreview(currAvatarIndex);
    }

    void SelectAvatar()
    {
        playerManager.SetSelectedAvatar(currAvatarIndex);

        SceneManager.LoadScene("HG");
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

        currPreviewAvatar = Instantiate(avatarPrefabs[index], previewPoint.position, previewPoint.rotation);
    }
}