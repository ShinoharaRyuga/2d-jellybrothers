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

    /// <summary>カメラが追従する対象を取得する </summary>
    Action _getCameraTarget = default;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    public int PlayerNumber => _playerController.PlayerNumber;

    /// <summary>カメラが追従する対象を取得する </summary>
    public Action GetCameraTarget { get => _getCameraTarget; set => _getCameraTarget = value; }
}
