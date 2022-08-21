using UnityEngine;

/// <summary>�t�F�[�h���ɍs���������܂Ƃ߂���Ă���N���X </summary>
public class Fade : MonoBehaviour
{
    StageManager _stageManager = default;

    /// <summary>�t�F�[�h���I������玩�g���폜���� </summary>
    public void ThisDestroy()
    {
        _stageManager.PlayerMove(true);
        Destroy(gameObject);
    }

    /// <summary>�t�F�[�h���Ƀ^�[�Q�b�g���擾���� </summary>
    public void GetCameraTarget()
    {
        Debug.Log("�^�[�Q�b�g���擾");
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _stageManager.GetCameraTarget();
    }
}
