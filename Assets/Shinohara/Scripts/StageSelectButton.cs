using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool _isHighlighted = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHighlighted = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHighlighted = false;
    }
}
