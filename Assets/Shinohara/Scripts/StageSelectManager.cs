using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

/// <summary>ステージ選択機能のクラス _selectColorsの添え字</summary>
[RequireComponent(typeof(PhotonView))]
public class StageSelectManager : MonoBehaviour
{
    /// <summary>元々のボタンの色　 </summary>
    const int ORIGINAL_BUTTON_COLOR = 2;
    /// <summary>全てのプレイヤーが同じボタンを選択した時の色 </summary>
    const int ALL_SELECT_COLOR = 3;
    [SerializeField, Header("シーン遷移するまでの時間")] float _waitTime = 3f;
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2", "元の色", "AllPlayer" })]
    [SerializeField, Header("セレクトカラー")] Color[] _selectColors = null;
    [PlayerNameArrayAttribute(new string[] { "Stage1", "Stage2", "Stage3", "Stage4", "Stage5" })]
    [SerializeField] StageSelectButton[] _stageSelectButton = default;

    PhotonView _myView => GetComponent<PhotonView>();

    private void Start()
    {
        //各ボタンのハイライト時の色を変更する
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
    /// ステージ選択ボタンにカーソルが乗ったら呼ばれる 
    /// </summary>
    /// <param name="index">変更するボタンの添え字</param>
    public void OnEnterStageSelectButton(StageSelectButton button)
    {
        button.IsSelect[PlayerData.Instance.PlayerNumber] = true;
        var playerNumber = PlayerData.Instance.PlayerNumber;
        object[] parameters = new object[] { button.Index, playerNumber, playerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.All, parameters);
    }

    /// <summary>
    /// ボタンの色を元々の色にする
    /// ステージ選択ボタンからカーソルが離れたら呼ばれる 
    /// </summary>
    /// <param name="index"></param>
    public void OnExitStageSelectButton(StageSelectButton button)
    {
        button.IsSelect[PlayerData.Instance.PlayerNumber] = false;
        object [] parameters = new object[] { button.Index, ORIGINAL_BUTTON_COLOR, PlayerData.Instance.PlayerNumber };
        _myView.RPC(nameof(AsyncButtonColor), RpcTarget.All, parameters);
    }

    /// <summary>全プレイヤーが同じボタンを選択している時のボタンの色を返す </summary>
    public Color GetAllSelectColor()
    {
        return _selectColors[ALL_SELECT_COLOR];
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
        StartCoroutine(WaitTransition(sceneName));
    }

    /// <summary>
    /// 渡された引数を元にボタンの色を変更する
    /// </summary>
    /// <param name="index">変更するボタンの添え字</param>
    /// <param name="playerNumber">変更後の色</param>
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
