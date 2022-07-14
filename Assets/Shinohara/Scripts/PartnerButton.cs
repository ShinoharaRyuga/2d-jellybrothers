using UnityEngine;

/// <summary>同時押しのボタンを作成するためのクラス </summary>
public class PartnerButton : MonoBehaviour
{
    [SerializeField, Tooltip("相方")] PartnerButton _partnerButton = default;
    /// <summary>プレイヤーが当たっているかどうか </summary>
    bool _isHit = false;
    public bool IsHit { get => _isHit; set => _isHit = value; }
    /// <summary>ギミックを参照する為 </summary>
    DoubleButton _doubleButton => transform.parent.GetComponent<DoubleButton>(); 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _doubleButton.Player.ToString())
        {
            _isHit = true;

            //ギミックオブジェクトのゲーム開始時の状態によって処理を変更する
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
