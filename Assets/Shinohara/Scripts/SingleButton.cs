using UnityEngine;

/// <summary>��̃{�^���Ńh�A���J���邱�Ƃ��o����{�^�� </summary>
public class SingleButton : MonoBehaviour
{
    [SerializeField, Header("�{�^���������ꂽ���ɓ��삷��M�~�b�N")] StageGimmick _gimmickObject = default;
    [SerializeField, Header("��������v���C���[")] Player _player = default;

    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    private void OnValidate()
    {
        //��������v���C���[�ɂ���ă{�^���̐F��ύX����
        if (_player == Player.Player1)
        {
            _spriteRenderer.color = Color.red;
        }
        else
        {
            _spriteRenderer.color = Color.blue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�{�^���������ꂽ
        SyncGimmick(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //�v���C���[���{�^�����痣�ꂽ
        SyncGimmick(collision);
    }

    /// <summary> �M�~�b�N�I�u�W�F�N�g�̏�Ԃ𓯊�����  </summary>
    void SyncGimmick(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            _gimmickObject.View.RPC("ChangeActive", Photon.Pun.RpcTarget.All);
        }
    }
}

public enum Player
{
    Player1,
    Player2,
}