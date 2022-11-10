using UnityEngine;

/// <summary>フェードイン中に行う処理をまとめたクラス </summary>
public class FadeIn : MonoBehaviour
{
    /// <summary>遷移先のシーン名 </summary>
    string _sceneName = "";
    /// <summary>遷移先のシーン名 </summary>
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    /// <summary>フェードイン中にシーン遷移する</summary>
    public void Transition()
    {
        NetworkManager.SceneTransition(_sceneName);
    }
}
