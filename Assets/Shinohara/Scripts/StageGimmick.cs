using Photon.Pun;
using UnityEngine;


/// <summary>各ギミックの表示・非表示を変更する </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class StageGimmick : MonoBehaviour
{
    ///　<summary>
    ///　<para>ゲーム開始時のオブジェクト状態</para>
    ///　<para>ゲーム開始時のオブジェクト状態を設定するのは状態によってボタンの処理を変える為</para>
    ///　ボタンが押された時...
    ///　<para>true = ギミックオブジェクトを非表示にする 例　ドアを開ける</para>
    ///　<para>false = ギミックオブジェクトを表示にする　例　足場を出現させる</para>
    /// </summary>
    [SerializeField, Header("ゲーム開始時のオブジェクト状態")] bool _isStartActive = true;
    [SerializeField, Header("扉の色")] DoorColor _doorColor = DoorColor.Red;
    /// <summary>現在のオブジェクト状態 </summary>
    bool _isCurrentActive = true;
    PhotonView _view => GetComponent<PhotonView>();
    BoxCollider2D _bc2D => GetComponent<BoxCollider2D>();
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    public PhotonView View { get => _view; }
    /// <summary>ゲーム開始時のアクティブ状態 </summary>
    public bool IsStartActive { get => _isStartActive; }

    private void OnValidate()
    {
        //ゲーム開始時のギミック状態を変更する
        if (_isStartActive)
        {
            _bc2D.enabled = true;
            _spriteRenderer.enabled = true;
            _isCurrentActive = true;
        }
        else
        {
            _bc2D.enabled = false;
            _spriteRenderer.enabled = false;
            _isCurrentActive = false;
        }

        //ドアの色を変更する
        if (_doorColor == DoorColor.Red)
        {
            _spriteRenderer.color = Color.red;
        }
        else if (_doorColor == DoorColor.Blue)
        {
            _spriteRenderer.color= Color.blue;
        }
    }

    /// <summary>
    /// ボタンが押された時に呼ばれる
    /// <para>オブジェクトの状態によって表示・非表示を変更する</para>
    /// </summary>
    [PunRPC]
    void ChangeActive()
    {
        if (_isCurrentActive) //非表示にする
        {
            ActiveFalse();
        }
        else //表示する
        {
            ActiveTrue();
        }
    }  

    /// <summary>
    /// 同時押し用ボタンが押された時に呼ばれる　(PartnerButton.cs)
    /// <para>オブジェクトを非表示にする</para>
    /// </summary>
    [PunRPC]
    void ActiveFalse()
    {
        _bc2D.enabled = false;
        _spriteRenderer.enabled = false;
        _isCurrentActive = false;
    }

    /// <summary>
    /// 同時押し用ボタンが押された時に呼ばれる (PartnerButton.cs)
    ///  <para>オブジェクトを表示する</para>
    /// </summary>
    [PunRPC]
    void ActiveTrue()
    {
        _bc2D.enabled = true;
        _spriteRenderer.enabled = true;
        _isCurrentActive = true;
    }

    public enum DoorColor
    {
        Red,
        Blue,
    }
}
