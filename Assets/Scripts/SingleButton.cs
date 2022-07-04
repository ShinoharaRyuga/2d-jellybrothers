using UnityEngine;

public class SingleButton : MonoBehaviour
{
    [SerializeField, Tooltip("�{�^���������ꂽ���ɊJ���h�A")] EnvironmentGimmickBase _gimmickObject = default;
    [SerializeField, Tooltip("��������v���C���[")] Player _player = default;
    [SerializeField, Tooltip("�J�n���̃A�N�e�B�u���")] bool _isActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            if (_isActive)
            {
                _gimmickObject.View.RPC("SetActiveFalse", Photon.Pun.RpcTarget.All);
            }
            else
            {
                _gimmickObject.View.RPC("SetActiveTrue", Photon.Pun.RpcTarget.All);
            }
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == _player.ToString())
        {
            if (_isActive)
            {
                _gimmickObject.View.RPC("SetActiveTrue", Photon.Pun.RpcTarget.All);
            }
            else
            {
                _gimmickObject.View.RPC("SetActiveFalse", Photon.Pun.RpcTarget.All);
            }
        }
    }
}

public enum Player
{
    Player1,
    Player2,
}