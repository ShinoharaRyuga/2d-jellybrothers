using UnityEngine;

/// <summary>�t�F�[�h�C�����ɍs���������܂Ƃ߂��N���X </summary>
public class FadeIn : MonoBehaviour
{
    /// <summary>�J�ڐ�̃V�[���� </summary>
    string _sceneName = "";
    /// <summary>�J�ڐ�̃V�[���� </summary>
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    /// <summary>�t�F�[�h�C�����ɃV�[���J�ڂ���</summary>
    public void Transition()
    {
        if (_sceneName == "")       //�X�e�[�W�I���V�[���ɑJ�ڂ���
        {
            NetworkManager.SceneTransition();
        }
        else
        {
            NetworkManager.SceneTransition(_sceneName);
        }
    }
}
