using UnityEngine;

/// <summary>ŠŠ‚é°‚ÌƒNƒ‰ƒX </summary>
public class SlideFloorGmmick : MonoBehaviour
{
    [SerializeField, Header("ŠŠ‚é°‚ÅŠ|‚¯‚é—Í"), Range(0.01f, 0.1f)] float _slideFloorPower = 0.01f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            player.GetSlidePower(_slideFloorPower);
            player.OnSlideFloor = true;
        }
    }
}
