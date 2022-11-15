using UnityEngine;

/// <summary>
/// 同時押しボタンのギミックを設定をする 
/// PartnerButton.csが参照している
/// </summary>
public class DoubleButton : MonoBehaviour
{
    [SerializeField, Header("動作するギミック")] StageGimmick _gimmick = default;
    [SerializeField, Header("反応するプレイヤー")] Player _player = default;

    public StageGimmick Gimmick { get => _gimmick; }
    public Player Player { get => _player; }
}
