using UnityEngine;

/// <summary>フェード中に行う処理がまとめるられているクラス </summary>
public class Fade : MonoBehaviour
{
    StageManager _stageManager = default;

    /// <summary>フェードが終わったら自身を削除する </summary>
    public void ThisDestroy()
    {
        _stageManager.PlayerMove(true);
        Destroy(gameObject);
    }

    /// <summary>フェード中にターゲットを取得する </summary>
    public void GetCameraTarget()
    {
        Debug.Log("ターゲットを取得");
        _stageManager = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>();
        _stageManager.GetCameraTarget();
    }
}
