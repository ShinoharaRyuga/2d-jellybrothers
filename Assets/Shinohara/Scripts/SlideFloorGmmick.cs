using UnityEngine;

/// <summary>���鏰�̃N���X </summary>
public class SlideFloorGmmick : MonoBehaviour
{
    [SerializeField, Header("���鏰�Ŋ|�����"), Range(0.01f, 0.1f)] float _slideFloorPower = 0.01f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.GetSlidePower(_slideFloorPower);
            player.OnSlideFloor = true;
        }
    }
}
