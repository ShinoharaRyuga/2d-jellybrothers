using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>待機シーンに遷移する </summary>
    public void WaitingRoomScene()
    {
        SceneManager.LoadScene("WaitingRoomScene");
    }

    /// <summary>ゲームシーンに遷移する </summary>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
