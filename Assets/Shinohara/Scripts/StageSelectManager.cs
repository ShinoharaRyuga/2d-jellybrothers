using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;


/// <summary>�X�e�[�W�I���@�\�̃N���X </summary>
public class StageSelectManager : MonoBehaviour
{
    [SerializeField, Header("�V�[���J�ڂ���܂ł̎���")] float _waitTime = 3f;
    [SerializeField, Header("1P�Z���N�g�J���[")] Color _1PColor = default;
    [SerializeField, Header("2P�Z���N�g�J���[")] Color _2PColor = default;
    [SerializeField] Button[] _stageSelectButton = default;
  
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

    /// <summary> </summary>
    /// <param name="sceneName"></param>
    public void StageSelect(string sceneName)
    {
        StartCoroutine(WaitTransition(sceneName));
    }


    IEnumerator WaitTransition(string sceneName)
    {
        yield return new WaitForSeconds(_waitTime);
        PhotonNetwork.LoadLevel(sceneName);
    }
}
