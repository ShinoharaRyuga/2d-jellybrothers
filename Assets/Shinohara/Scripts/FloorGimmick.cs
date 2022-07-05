using Photon.Pun;
using UnityEngine;

/// <summary>�����o������������肷�� </summary>
public class FloorGimmick : EnvironmentGimmickBase
{
    [SerializeField, Tooltip("�I�u�W�F�N�g�𓧖��ɂ����")] SpriteRenderer _spriteRenderer = default;
    [SerializeField, Tooltip("�����蔻���ω�������")] BoxCollider2D _bc2D = default;
    [SerializeField, Tooltip("�Q�[���J�n���̏��")] bool _isActive = false;


    private void Start()
    {
        if (!_isActive)
        {
            _spriteRenderer.enabled = false;
            _bc2D.enabled = false;
        }
    }

    [PunRPC]
    public override void SetActiveFalse()
    {
        _spriteRenderer.enabled = false;
        _bc2D.enabled = false;
    }

    [PunRPC]
    public override void SetActiveTrue()
    {
        _spriteRenderer.enabled = true;
        _bc2D.enabled = true;
    }
}
