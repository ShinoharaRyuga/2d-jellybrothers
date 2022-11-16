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

    /// <summary>�J�������Ǐ]����Ώۂ��擾����ۂɌĂԃf���P�[�g </summary>
    event Action _delGetCameraTarget = default;

    public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

    public int PlayerNumber => _playerController.PlayerNumber;

    /// <summary>�J�������Ǐ]����Ώۂ��擾����ۂɌĂԃf���P�[�g </summary>
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
