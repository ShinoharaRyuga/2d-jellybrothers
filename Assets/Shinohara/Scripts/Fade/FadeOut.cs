using UnityEngine;

/// <summary>フェードアウトで使用するクラス </summary>
public class FadeOut : MonoBehaviour
{
    /// <summary>フェード中に各プレイヤーを取得する </summary>
    public void GetTarget()
    {
        var gameCamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<CameraTarget>();
        gameCamera.GetTarget();
    }

    /// <summary>自身を削除する </summary>
    public void DeleteThis()
    {
        Destroy(gameObject);
    }
}
