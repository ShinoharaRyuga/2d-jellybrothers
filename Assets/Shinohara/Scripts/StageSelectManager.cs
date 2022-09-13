using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>�X�e�[�W�I���@�\�̃N���X _selectColors�̓Y����</summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    /// <summary>���X�̃{�^���̐F�@ </summary>
    const int ORIGINAL_BUTTON_COLOR = 2;
   
    [SerializeField, Header("�V�[���J�ڂ���܂ł̎���")] float _waitTime = 3f;
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2", "���̐F" })]
    [SerializeField, Header("�Z���N�g�J���[")] Color[] _selectColors = null;
    [SerializeField] Button[] _stageSelectButton = default;

    PhotonView _myView => GetComponent<PhotonView>();
    private void Start()
    {
        //�e�{�^���̃n�C���C�g���̐F��ύX����
        foreach (var button in _stageSelectButton)
        {
            var colorBlock = button.colors;
            colorBlock.highlightedColor = _selectColors[PlayerData.Instance.PlayerNumber];
            button.colors = colorBlock;
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
    /// �X�e�[�W�I���{�^���Ƀ}�E�X���������Ă΂�� 
    /// </summary>
    /// <param name="index">�ύX����{�^���̓Y����</param>
    public void OnEnterStageSelectButton(int index)
    {
        // TODO: Event �܂��� RPC �ő���ɂǂꂪ�I�΂ꂽ������
        object[] parameters = new object[] { index, PlayerData.Instance.PlayerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.Others, parameters);
    }

    /// <summary>
    /// �{�^���̐F�����X�̐F�ɂ���
    /// �X�e�[�W�I���{�^������}�E�X�����ꂽ��Ă΂�� 
    /// </summary>
    /// <param name="index"></param>
    public void OnExitStageSelectButton(int index)
    {
        object [] parameters = new object[] { index, ORIGINAL_BUTTON_COLOR };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.Others, parameters);
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
        PhotonNetwork.LoadLevel(sceneName);
    }

    /// <summary>
    /// �n���ꂽ���������Ƀ{�^���̐F��ύX����
    /// </summary>
    /// <param name="index">�ύX����{�^���̓Y����</param>
    /// <param name="playerNumber">�ύX��̐F</param>
    [PunRPC]
    void AsyncButtonColor(int index, int playerNumber)
    {
        var selectButton = _stageSelectButton[index];
        var colorBlock = selectButton.colors;
        colorBlock.normalColor = _selectColors[playerNumber];
        selectButton.colors = colorBlock;
    }
}
