using System;

/// <summary>
/// Player���ێ����Ă����N���X
/// �V�[���J�ڎ��ɂ��̏����g���Ď��@�𐶐�����
/// </summary>
public class PlayerData 
{
    static PlayerData _instance = new PlayerData();
    static public PlayerData Instance => _instance;

    PlayerController _playerController = default;

    /// <summary>�J�������Ǐ]����Ώۂ��擾���� </summary>
    Action _getCameraTarget = default;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    public int PlayerNumber => _playerController.PlayerNumber;

    /// <summary>�J�������Ǐ]����Ώۂ��擾���� </summary>
    public Action GetCameraTarget { get => _getCameraTarget; set => _getCameraTarget = value; }
}
