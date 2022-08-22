using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>シーン遷移する為のクラス </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>待機シーンに遷移する </summary>
    public void WaitingRoomScene()
    {
        SceneManager.LoadScene("WaitingRoomScene");
    }
}
