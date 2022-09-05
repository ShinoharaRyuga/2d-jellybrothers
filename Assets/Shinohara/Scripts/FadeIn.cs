using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public void StageSelect()
    {
        var StageSelect = GameObject.Find("SelectManager").GetComponent<StageSelectManager>();
        
    }
}
