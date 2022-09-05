using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;



/// <summary>ステージ選択機能のクラス </summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    [SerializeField, Header("シーン遷移するまでの時間")] float _waitTime = 3f;
    [SerializeField, Header("1Pセレクトカラー")] Color _1PColor = default;
    [SerializeField, Header("2Pセレクトカラー")] Color _2PColor = default;
    [SerializeField] Button[] _stageSelectButton = default;
  
    PhotonView _myView => GetComponent<PhotonView>();
    private void Start()
    {
        //プレイヤーが選択しているボタンを色を変更する
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
    /// プレイヤーが遊びたいステージを選択する
    /// onClickで呼ばれる
    /// </summary>
    /// <param name="sceneName">選択されたシーン名</param>
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
        // TODO: Event または RPC で相手にどれが選ばれたか送る
    }

    /// <summary>一定時間後シーン遷移する </summary>
    IEnumerator WaitTransition(string sceneName)
    {
        yield return new WaitForSeconds(_waitTime);
        PhotonNetwork.LoadLevel(sceneName);
    }

    /// <summary>マスタークライアントではないプレイヤー(2P)
    /// がステージを選択した場合に呼ばれる 
    /// </summary>
    [PunRPC]
    void SceneChange(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
