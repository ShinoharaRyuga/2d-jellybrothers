using UnityEngine;

public class PartnerButton : MonoBehaviour
{
    [SerializeField] PartnerButton _partnerButton = default;
    DoubleButton _doubleButton => transform.parent.GetComponent<DoubleButton>(); 
    bool _isHit = false;
    public bool IsHit { get => _isHit; set => _isHit = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _doubleButton.Player.ToString())
        {
            _isHit = true;

            if (_partnerButton.IsHit && _isHit && _doubleButton.Gimmick.IsStartActive)
            {
                Debug.Log("Enter 非表示");
                _doubleButton.Gimmick.View.RPC("ActiveFalse", Photon.Pun.RpcTarget.All);
            }
            else if (_partnerButton.IsHit && _isHit && !_doubleButton.Gimmick.IsStartActive)
            {
                Debug.Log("Enter　表示");
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
                Debug.Log("Trigger 非表示");
                _doubleButton.Gimmick.View.RPC("ActiveFalse", Photon.Pun.RpcTarget.All);
            }
            else if (_doubleButton.Gimmick.IsStartActive)
            {
                Debug.Log("Trigger　表示");
                _doubleButton.Gimmick.View.RPC("ActiveTrue", Photon.Pun.RpcTarget.All);
            }
        }
    }
}
