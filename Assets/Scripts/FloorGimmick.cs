using Photon.Pun;
using UnityEngine;

public class FloorGimmick : EnvironmentGimmickBase
{
    [SerializeField] SpriteRenderer _spriteRenderer = default;
    [SerializeField] BoxCollider2D _bc2D = default;
    [SerializeField] bool _isActive = false;


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
