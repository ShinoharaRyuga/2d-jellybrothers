using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�V�[���J�ڂ���N���X </summary>
public class StandByRoomTransition : MonoBehaviour
{
    /// <summary>�ҋ@�V�[���ɑJ�ڂ��� </summary>
    public void StandByRoomSceneTransition()
    {
        SceneManager.LoadScene("StandByRoomScene");
    }
}
