using UnityEngine;

/// <summary>�t�F�[�h�A�E�g�Ŏg�p����N���X </summary>
public class FadeOut : MonoBehaviour
{
    /// <summary>�t�F�[�h���Ɋe�v���C���[���擾���� </summary>
    public void GetTarget()
    {
        var gameCamera = GameObject.FindGameObjectWithTag("GameCamera").GetComponent<CameraTarget>();
        gameCamera.GetTarget();
    }

    /// <summary>���g���폜���� </summary>
    public void DeleteThis()
    {
        Destroy(gameObject);
    }
}
