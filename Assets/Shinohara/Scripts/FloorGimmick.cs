using Photon.Pun;
using UnityEngine;

/// <summary>床を出したり消したりする </summary>
public class FloorGimmick : EnvironmentGimmickBase
{
    [SerializeField, Tooltip("オブジェクトを透明にする為")] SpriteRenderer _spriteRenderer = default;
    [SerializeField, Tooltip("当たり判定を変化させる")] BoxCollider2D _bc2D = default;
    [SerializeField, Tooltip("ゲーム開始時の状態")] bool _isActive = false;


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
