using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>�X�e�[�W�I���@�\�̃N���X _selectColors�̓Y����</summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    /// <summary>���X�̃{�^���̐F�@ </summary>
    const int ORIGINAL_BUTTON_COLOR = 2;
    /// <summary>�S�Ẵv���C���[�������{�^����I���������̐F </summary>
    const int ALL_SELECT_COLOR = 3;
    [SerializeField, Header("�V�[���J�ڂ���܂ł̎���")] float _waitTime = 3f;
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2", "���̐F", "AllPlayer" })]
    [SerializeField, Header("�Z���N�g�J���[")] Color[] _selectColors = null;
    [PlayerNameArrayAttribute(new string[] { "Stage1", "Stage2", "Stage3", "Stage4", "Stage5" })]
    [SerializeField] StageSelectButton[] _stageSelectButton = default;

    PhotonView _myView => GetComponent<PhotonView>();

    private void Start()
    {
        //�e�{�^���̃n�C���C�g���̐F��ύX����
        for (var i = 0; i < _stageSelectButton.Length; i++)
        {
            var button = _stageSelectButton[i].GetComponent<Button>();
            var colorBlock = button.colors;
            colorBlock.highlightedColor = _selectColors[PlayerData.Instance.PlayerNumber];
            button.colors = colorBlock;
            _stageSelectButton[i].Index = i;
        }
    }

    /// <summary>
    /// �v���C���[���V�т����X�e�[�W��I������
    /// onClick�ŌĂ΂��
    /// </summary>
    /// <param name="sceneName">�I�����ꂽ�V�[����</param>
    public void StageSelect(string sceneName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(WaitTransition(sceneName));
        }
        else
        {
            object[] parameters = new object[] { sceneName };
            _myView.RPC(nameof(SceneChange), RpcTarget.Others, parameters);
        }
    }

    /// <summary>
    /// ����{�^���̐F��ύX����
    /// �X�e�[�W�I���{�^���ɃJ�[�\�����������Ă΂�� 
    /// </summary>
    /// <param name="index">�ύX����{�^���̓Y����</param>
    public void OnEnterStageSelectButton(StageSelectButton button)
    {
        button.IsSelect[PlayerData.Instance.PlayerNumber] = true;
        var playerNumber = PlayerData.Instance.PlayerNumber;
        object[] parameters = new object[] { button.Index, playerNumber, playerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.All, parameters);
    }

    /// <summary>
    /// �{�^���̐F�����X�̐F�ɂ���
    /// �X�e�[�W�I���{�^������J�[�\�������ꂽ��Ă΂�� 
    /// </summary>
    /// <param name="index"></param>
    public void OnExitStageSelectButton(StageSelectButton button)
    {
        button.IsSelect[PlayerData.Instance.PlayerNumber] = false;
        object [] parameters = new object[] { button.Index, ORIGINAL_BUTTON_COLOR, PlayerData.Instance.PlayerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.All, parameters);
    }

    /// <summary>�S�v���C���[�������{�^����I�����Ă��鎞�̃{�^���̐F��Ԃ� </summary>
    public Color GetAllSelectColor()
    {
        return _selectColors[ALL_SELECT_COLOR];
    }

    /// <summary>��莞�Ԍ�V�[���J�ڂ��� </summary>
    IEnumerator WaitTransition(string sceneName)
    {
        yield return new WaitForSeconds(_waitTime);
        PhotonNetwork.LoadLevel(sceneName);
    }

    /// <summary>�}�X�^�[�N���C�A���g�ł͂Ȃ��v���C���[(2P)
    /// ���X�e�[�W��I�������ꍇ�ɌĂ΂�� 
    /// </summary>
    [PunRPC]
    void SceneChange(string sceneName)
    {
        StartCoroutine(WaitTransition(sceneName));
    }

    /// <summary>
    /// �n���ꂽ���������Ƀ{�^���̐F��ύX����
    /// </summary>
    /// <param name="index">�ύX����{�^���̓Y����</param>
    /// <param name="playerNumber">�ύX��̐F</param>
    [PunRPC]
    void AsyncButtonColor(int index, int colorIndex, int playerNumber)
    {
        _stageSelectButton[index].IsSelect[playerNumber] = true;
        var selectButton = _stageSelectButton[index].GetComponent<Button>();
        var colorBlock = selectButton.colors;
        colorBlock.normalColor = _selectColors[colorIndex];
        selectButton.colors = colorBlock;

        _stageSelectButton[index].ChangeAllSelectColor();
    }
}
