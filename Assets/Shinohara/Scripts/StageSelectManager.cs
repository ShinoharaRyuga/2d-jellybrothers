using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;



/// <summary>�X�e�[�W�I���@�\�̃N���X </summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڂ���܂ł̎���")] float _waitTime = 3f;
    [SerializeField, Header("1P�Z���N�g�J���[")] Color _1PColor = default;
    [SerializeField, Header("2P�Z���N�g�J���[")] Color _2PColor = default;
    [SerializeField] Button[] _stageSelectButton = default;
  
    PhotonView _myView => GetComponent<PhotonView>();
    private void Start()
    {
        //�v���C���[���I�����Ă���{�^����F��ύX����
        if (PlayerData.Instance.PlayerNumber == 0)  //1P
        {
            foreach (var button in _stageSelectButton)
            {
                var colorBlock = button.colors;
                colorBlock.highlightedColor = _1PColor;
                button.colors = colorBlock;
            }
        }
        else   //2P
        {
            foreach (var button in _stageSelectButton)
            {
                var colorBlock = button.colors;
                colorBlock.highlightedColor = _2PColor;
                button.colors = colorBlock;
            }
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
            Debug.Log("hit");
            object[] parameters = new object[] { sceneName };
            _myView.RPC("SceneChange", RpcTarget.Others, parameters);
        }
    }

    public void OnEnterStageSelectButton(Button button)
    {
        Debug.Log($"Mouse Enter: {button.gameObject.name}");
        // TODO: Event �܂��� RPC �ő���ɂǂꂪ�I�΂ꂽ������
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
}
