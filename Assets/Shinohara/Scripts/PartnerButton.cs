using UnityEngine;

/// <summary>���������̃{�^�����쐬���邽�߂̃N���X </summary>
public class PartnerButton : MonoBehaviour
{
    [SerializeField, Tooltip("����")] PartnerButton _partnerButton = default;
    /// <summary>�v���C���[���������Ă��邩�ǂ��� </summary>
    bool _isHit = false;
    public bool IsHit { get => _isHit; set => _isHit = value; }
    /// <summary>�M�~�b�N���Q�Ƃ���� </summary>
    DoubleButton _doubleButton => transform.parent.GetComponent<DoubleButton>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _doubleButton.Player.ToString())
        {
            _isHit = true;

            //�M�~�b�N�I�u�W�F�N�g�̃Q�[���J�n���̏�Ԃɂ���ď�����ύX����
            if (_partnerButton.IsHit && _isHit && _doubleButton.Gimmick.IsStartActive)
            {
                _doubleButton.Gimmick.View.RPC("ActiveFalse", Photon.Pun.RpcTarget.All);
            }
            else if (_partnerButton.IsHit && _isHit && !_doubleButton.Gimmick.IsStartActive)
            {
                _doubleButton.Gimmick.View.RPC("ActiveTrue", Photon.Pun.RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _doubleButton.Player.ToString())
        {
            _isHit = false;

            if  (!_doubleButton.Gimmick.IsStartActive)
            {
                _doubleButton.Gimmick.View.RPC("ActiveFalse", Photon.Pun.RpcTarget.All);
            }
            else if (_doubleButton.Gimmick.IsStartActive)
            {
                _doubleButton.Gimmick.View.RPC("ActiveTrue", Photon.Pun.RpcTarget.All);
            }
        }
    }
}
