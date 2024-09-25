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
    public GameObject waitPanel;
    public Button startButton;
    public Button backButton;

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
        startButton.onClick.AddListener(StartWork);
        backButton.onClick.AddListener(SelectCancle);


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

        waitPanel.SetActive(true);

        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);
        selectButton.gameObject.SetActive(false);

    }

    void GoLobby()
    {
        SceneManager.LoadScene("H_LobbyScene");
    }

    void StartWork()
    {
        SceneManager.LoadScene("PlayScene");
    }
    void ShowAvatarPreview(int index)
    {
        if (currPreviewAvatar != null)
        {
            Destroy(currPreviewAvatar);
        }

        currPreviewAvatar = Instantiate(playerManager.avatarPrefabs[index], previewPoint.position, previewPoint.rotation);
    }

    void SelectCancle()
    {
        waitPanel.SetActive(false);

        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        selectButton.gameObject.SetActive(true);
    }
}