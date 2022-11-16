using System;

/// <summary>
/// Player情報保持しておくクラス
/// シーン遷移時にこの情報を使って自機を生成する
/// </summary>
public class PlayerData
{
    static PlayerData _instance = new PlayerData();
    static public PlayerData Instance => _instance;

    PlayerController _playerController = default;

    /// <summary>カメラが追従する対象を取得する際に呼ぶデリケート </summary>
    event Action _delGetCameraTarget = default;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    public int PlayerNumber => _playerController.PlayerNumber;

    /// <summary>カメラが追従する対象を取得する際に呼ぶデリケート </summary>
    public event Action DelGetCameraTarget
    {
        add { _delGetCameraTarget += value; }
        remove { _delGetCameraTarget -= value; }
    }

    public void GetCameraTargetInvoke()
    {
        _delGetCameraTarget?.Invoke();
    }
}
