using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>シーン遷移するクラス </summary>
public class StandByRoomTransition : MonoBehaviour
{
    /// <summary>待機シーンに遷移する </summary>
    public void StandByRoomSceneTransition()
    {
        SceneManager.LoadScene("StandByRoomScene");
    }
}
