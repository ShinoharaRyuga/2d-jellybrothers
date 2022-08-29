using UnityEngine;

/// <summary>�Q�[���V�[���̊Ǘ��N���X </summary>
public class StageManager : MonoBehaviour
{
    [PlayerNameArrayAttribute(new string[] { "Player1", "Player2" })]
    [SerializeField, Header("�Q�[���J�n���̃X�^�[�g�ʒu"), Tooltip("�Y���� 0=Player1 1=Player2")] Transform[] _startSpwanPoint = new Transform[2];

    private void Start()
    {
        var playerNumber = PlayerData.Instance.PlayerController.PlayerNumber;
        NetworkManager.PlayerInstantiate(playerNumber, _startSpwanPoint[playerNumber].position);
    }
}
