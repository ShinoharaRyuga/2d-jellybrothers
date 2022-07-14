using UnityEngine;

/// <summary>一つのボタンでドアを開けることが出来るボタン </summary>
public class SingleButton : MonoBehaviour
{
    [SerializeField, Header("ボタンが押された時に動作するギミック")] StageGimmick _gimmickObject = default;
    [SerializeField, Header("反応するプレイヤー")] Player _player = default;

    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    private void OnValidate()
    {
        //反応するプレイヤーによってボタンの色を変更する
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
        //ボタンが押された
        SyncGimmick(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //プレイヤーがボタンから離れた
        SyncGimmick(collision);
    }

    /// <summary> ギミックオブジェクトの状態を同期する  </summary>
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