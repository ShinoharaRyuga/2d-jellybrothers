using UnityEngine;

/// <summary>
/// ���������{�^���̃M�~�b�N��ݒ������ 
/// PartnerButton.cs���Q�Ƃ��Ă���
/// </summary>
public class DoubleButton : MonoBehaviour
{
    [SerializeField, Header("���삷��M�~�b�N")] StageGimmick _gimmick = default;
    [SerializeField, Header("��������v���C���[")] Player _player = default;

    public StageGimmick Gimmick { get => _gimmick; }
    public Player Player { get => _player; }
}
