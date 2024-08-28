using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 특정 씬으로 전환하는 메서드
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 특정 인덱스의 씬으로 전환하는 메서드
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 현재 씬을 다시 로드하는 메서드
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // 특정 키를 눌렀을 때 씬 전환을 위한 Update 메서드
    void Update()
    {
        // 예: R 키를 눌러 현재 씬 다시 로드
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        // 예: N 키를 눌러 다음 씬으로 전환
        if (Input.GetKeyDown(KeyCode.N))
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            ChangeScene(nextSceneIndex);
        }
    }
}
