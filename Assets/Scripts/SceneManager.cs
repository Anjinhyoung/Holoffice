using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Ư�� ������ ��ȯ�ϴ� �޼���
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Ư�� �ε����� ������ ��ȯ�ϴ� �޼���
    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // ���� ���� �ٽ� �ε��ϴ� �޼���
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Ư�� Ű�� ������ �� �� ��ȯ�� ���� Update �޼���
    void Update()
    {
        // ��: R Ű�� ���� ���� �� �ٽ� �ε�
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }

        // ��: N Ű�� ���� ���� ������ ��ȯ
        if (Input.GetKeyDown(KeyCode.N))
        {
            int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
            ChangeScene(nextSceneIndex);
        }
    }
}
