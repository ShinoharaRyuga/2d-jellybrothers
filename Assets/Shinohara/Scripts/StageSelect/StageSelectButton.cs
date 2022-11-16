using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>�X�e�[�W�I���{�^�����Ǘ�����N���X </summary>
public class StageSelectButton : MonoBehaviour
{
    [SerializeField, Header("�V�[���ɑ��݂�����̂��A�^�b�`")] StageSelectManager _stageSelectManager;
    /// <summary>
    /// �e�v���C���[�̃}�E�X������Ă����true
    /// 0=1P 1=2P 
    /// </summary>
    bool[] _isSelect = new bool[2];
    int _index = 0;

    Button _button => GetComponent<Button>();
    Image _image => GetComponent<Image>();

    PhotonView _myView => GetComponent<PhotonView>();
    public bool[] IsSelect { get => _isSelect; set => _isSelect = value; }
    public int Index { get => _index; set => _index = value; }
    /// <summary>�S�v���C���[�̃J�[�\�����{�^���̂ɏ���Ă��� </summary>
    public bool _allPlayerSelect => IsSelect[0] && IsSelect[1];

    public void ChangeAllSelectColor()
    {
        if (_allPlayerSelect)
        {
            _myView.RPC(nameof(AsyncButtonColor), RpcTarget.All);
        }
        else
        {
            _myView.RPC(nameof(AsyncImageColor), RpcTarget.All);
        }
    }

    [PunRPC]
    void AsyncButtonColor()
    {
        _image.color = Color.cyan;
        var colorBlock = _button.colors;
        colorBlock.highlightedColor = Color.white;
        _button.colors = colorBlock;
    }

    [PunRPC]
    void AsyncImageColor()
    {
        _image.color = Color.white;
    }
}
