using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�V�[���J�ڂ���ׂ̃N���X </summary>
public class SceneChanger : MonoBehaviour
{
    /// <summary>�ҋ@�V�[���ɑJ�ڂ��� </summary>
    public void WaitingRoomScene()
    {
        SceneManager.LoadScene("WaitingRoomScene");
    }
}
