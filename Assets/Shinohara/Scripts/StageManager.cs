using UnityEngine;

/// <summary>ゲームシーンの管理クラス </summary>
public class StageManager : MonoBehaviour
{
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("ゲーム開始時のスタート位置"), Tooltip("添え字 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];

    private void Start()
    {
        var playerNumber = PlayerData.Instance.PlayerController.PlayerNumber;
        NetworkManager.PlayerInstantiate(playerNumber, _startSpwanPoint[playerNumber].position);
    }
}
