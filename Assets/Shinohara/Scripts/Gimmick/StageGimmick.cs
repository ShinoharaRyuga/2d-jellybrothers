using Photon.Pun;
using UnityEngine;


/// <summary>扉・床・足場ギミックの表示・非表示を変更する </summary>
[RequireComponent(typeof(BoxCollider2D), typeof(PhotonView), typeof(SpriteRenderer))]
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
    [SerializeField, Header("オブジェクトの色")] ObjectColor _objectColor = ObjectColor.Red;
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
        ChangeObjectStauts();

        //オブジェクトの色を変更する
        switch (_objectColor)
        {
            case ObjectColor.Red:
                _spriteRenderer.color = Color.red;
                break;
            case ObjectColor.Blue:
                _spriteRenderer.color = Color.blue;
                break;
            case ObjectColor.White:
                _spriteRenderer.color = Color.white;
                break;
        }
    }

    private void Start()
    {
        ChangeObjectStauts();
    }

    /// <summary>
    /// ボタンが押された時に呼ばれる
    /// <para>オブジェクトの状態によって表示・非表示を変更する</para>
    /// </summary>
    [PunRPC]
    public void ChangeActive()
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
    public void ActiveFalse()
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
    public void ActiveTrue()
    {
        _bc2D.enabled = true;
        _spriteRenderer.enabled = true;
        _isCurrentActive = true;
    }

    /// <summary>オブジェクトの状態を変更する</summary>
    void ChangeObjectStauts()
    {
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
    }

    /// <summary>オブジェクトの色 </summary>
    public enum ObjectColor
    {
        Red,
        Blue,
        White,
    }
}
