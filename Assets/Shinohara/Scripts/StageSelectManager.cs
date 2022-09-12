using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

/// <summary>ステージ選択機能のクラス _selectColorsの添え字</summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    /// <summary>元々のボタンの色　 </summary>
    const int ORIGINAL_BUTTON_COLOR = 2;
   
    [SerializeField, Header("シーン遷移するまでの時間")] float _waitTime = 3f;
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2", "元の色" })]
    [SerializeField, Header("セレクトカラー")] Color[] _selectColors = null;
    [SerializeField] Button[] _stageSelectButton = default;

    PhotonView _myView => GetComponent<PhotonView>();
    private void Start()
    {
        //各ボタンのハイライト時の色を変更する
        foreach (var button in _stageSelectButton)
        {
            var colorBlock = button.colors;
            colorBlock.highlightedColor = _selectColors[PlayerData.Instance.PlayerNumber];
            button.colors = colorBlock;
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
            object[] parameters = new object[] { sceneName };
            _myView.RPC(nameof(SceneChange), RpcTarget.Others, parameters);
        }
    }

    /// <summary>
    /// 相手ボタンの色を変更する
    /// ステージ選択ボタンにマウスが乗ったら呼ばれる 
    /// </summary>
    /// <param name="index">変更するボタンの添え字</param>
    public void OnEnterStageSelectButton(int index)
    {
        // TODO: Event または RPC で相手にどれが選ばれたか送る
        object[] parameters = new object[] { index, PlayerData.Instance.PlayerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.Others, parameters);
    }

    /// <summary>
    /// ボタンの色を元々の色にする
    /// ステージ選択ボタンからマウスが離れたら呼ばれる 
    /// </summary>
    /// <param name="index"></param>
    public void OnExitStageSelectButton(int index)
    {
        object [] parameters = new object[] { index, ORIGINAL_BUTTON_COLOR };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.Others, parameters);
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

    /// <summary>
    /// 渡された引数を元にボタンの色を変更する
    /// </summary>
    /// <param name="index">変更するボタンの添え字</param>
    /// <param name="playerNumber">変更後の色</param>
    [PunRPC]
    void AsyncButtonColor(int index, int playerNumber)
    {
        var selectButton = _stageSelectButton[index];
        var colorBlock = selectButton.colors;
        colorBlock.normalColor = _selectColors[playerNumber];
        selectButton.colors = colorBlock;
    }
}
