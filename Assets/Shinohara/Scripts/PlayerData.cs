/// <summary>
/// Player情報保持しておくクラス
/// シーン遷移時にこの情報を使って自機を生成する
/// </summary>
public class PlayerData 
{
    static PlayerData _instance = new PlayerData();
    static public PlayerData Instance => _instance;

    PlayerController _playerController = default;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    public int PlayerNumber => _playerController.PlayerNumber;
}
