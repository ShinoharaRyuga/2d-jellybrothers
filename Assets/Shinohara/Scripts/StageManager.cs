using UnityEngine;

/// <summary>ゲームシーンの管理クラス </summary>
public class StageManager : MonoBehaviour
{
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("ゲーム開始時のスタート位置"), Tooltip("添え字 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];
    [SerializeField, Tooltip("フェードアウトするプレハブ")] Fade _fadeOut = default;
    PlayerController _player = default;
    CameraTarget _cameraTarget => GetComponent<CameraTarget>();

    private void Start()
    {
        Instantiate(_fadeOut);
        var playerNumber = PlayerData.Instance.PlayerController.PlayerNumber;
        _player = NetworkManager.PlayerInstantiate(playerNumber, _startSpwanPoint[playerNumber].position).GetComponent<PlayerController>();
        _player.IsMove = false;
    }

    /// <summary>カメラのターゲット(Player)を取得する </summary>
    public void GetCameraTarget()
    {
        _cameraTarget.GetTarget();
    }

    /// <summary>プレイヤーの移動を制限・可能にする  </summary>
    public void PlayerMove(bool isMove)
    {
        _player.IsMove = isMove;
    }
}
