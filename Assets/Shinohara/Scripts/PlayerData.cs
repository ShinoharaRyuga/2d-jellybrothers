/// <summary>
/// Player���ێ����Ă����N���X
/// �V�[���J�ڎ��ɂ��̏����g���Ď��@�𐶐�����
/// </summary>
public class PlayerData 
{
    static PlayerData _instance = new PlayerData();
    static public PlayerData Instance => _instance;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    PlayerController _playerController = default;
}
