using System.Collections.Generic;
using UnityEngine;

/// <summary>��̃{�^���ŕ����̃M�~�b�N�𓮍삷��׃N���X </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class StageGimmicksUse : MonoBehaviour
{
    [SerializeField, Header("�{�^���������ꂽ���ɓ��삷��M�~�b�N")] List<StageGimmick> _gimmickObjects = default;
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
            foreach (var gimmick in _gimmickObjects)
            {
                gimmick.View.RPC("ChangeActive", Photon.Pun.RpcTarget.All);
            }
        }
    }
}
