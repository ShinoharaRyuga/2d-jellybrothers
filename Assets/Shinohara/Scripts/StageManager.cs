using UnityEngine;

/// <summary>�Q�[���V�[���̊Ǘ��N���X </summary>
public class StageManager : MonoBehaviour
{
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("�Q�[���J�n���̃X�^�[�g�ʒu"), Tooltip("�Y���� 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];
    [SerializeField, Tooltip("�t�F�[�h�A�E�g�̃v���n�u")] FadeOut _fadeOutPrefab = default;
    [SerializeField, Tooltip("�t�F�[�h�C�����s���v���n�u")] FadeIn _fadeInPrefab = default;

    private void Start()
    {
        var playerNumber = PlayerData.Instance.PlayerController.PlayerNumber;
        NetworkManager.PlayerInstantiate(playerNumber, _startSpwanPoint[playerNumber].position);
        Instantiate(_fadeOutPrefab);
    }

    /// <summary>�X�e�[�W�N���A����</summary>
    public void StageClear()
    {
        var fadeInObj = Instantiate(_fadeInPrefab);
    }
}
