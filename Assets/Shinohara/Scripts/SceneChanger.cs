using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    /// <summary>�ҋ@�V�[���ɑJ�ڂ��� </summary>
    public void WaitingRoomScene()
    {
        SceneManager.LoadScene("WaitingRoomScene");
        Debug.Log("�Ă΂��");
    }

    /// <summary>�Q�[���V�[���ɑJ�ڂ��� </summary>
    public void SceneLoad(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
