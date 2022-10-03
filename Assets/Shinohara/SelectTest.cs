using UnityEngine;
using UnityEngine.UI;

public class SelectTest : MonoBehaviour
{
    /// <summary>
    /// 各プレイヤーのマウスが乗っていればtrue
    /// 0=1P 1=2P 
    /// </summary>
    bool[] _isSelect = new bool[2];
    int _index = 0;

    Button _button => GetComponent<Button>();
    Image _image => GetComponent<Image>();
    public bool[] IsSelect { get => _isSelect; set => _isSelect = value; }
    public int Index { get => _index; set => _index = value; }
    /// <summary>全プレイヤーのカーソルがボタンのに乗っている </summary>
    public bool _allPlayerSelect => IsSelect[0] && IsSelect[1];

    private void Start()
    {
        _isSelect[1] = true;
    }

    private void Update()
    {
        if (_allPlayerSelect)
        {
            _image.color = Color.yellow;
        }
    }

    public void OnEnterStageSelectButton()
    {
        _isSelect[0] = true;
        var colorBlock = _button.colors;
        colorBlock.normalColor = Color.red;
        _button.colors = colorBlock;
    }

    public void OnExitStageSelectButton()
    {
        _isSelect[0] = false;
    }
}
