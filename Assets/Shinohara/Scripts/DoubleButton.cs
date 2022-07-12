using UnityEngine;

public class DoubleButton : MonoBehaviour
{
    [SerializeField] StageGimmick gimmick = default;
    [SerializeField, Tooltip("��������v���C���[")] Player _player = default;
    [SerializeField] SpriteRenderer _button1Renderer = default;
    [SerializeField] SpriteRenderer _button2Renderer = default;
    public StageGimmick Gimmick { get => gimmick; }
    public Player Player { get => _player; }
  
    //private void OnValidate()
    //{
    //    //��������v���C���[�ɂ���ă{�^���̐F��ύX����
    //    if (_player == Player.Player1)
    //    {
    //        _button1Renderer.color = Color.red;
    //        _button1Renderer.color = Color.red;
    //    }
    //    else
    //    {
    //        _button2Renderer.color = Color.blue;
    //        _button2Renderer.color = Color.blue;
    //    }
    //}
}
